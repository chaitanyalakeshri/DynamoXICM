# current_directory = File.dirname(__FILE__)
# database_file= File.join(current_directory, 'database/open_database.rb')
paths = [
  'C:/Program Files/Autodesk/InfoWorks ICM Ultimate 2025/lib/ruby/2.4.0/x64-mswin64_140',
  'C:/Program Files/Autodesk/InfoWorks ICM Ultimate 2025/lib/ruby/2.4.0',
  'C:/Program Files/Autodesk/InfoWorks ICM Ultimate 2025/lib/ruby/vendor_ruby',
  'C:/Program Files/Autodesk/InfoWorks ICM Ultimate 2025/lib/ruby/vendor_ruby/2.4.0/x64-vcruntime140',
  'C:/Program Files/Autodesk/InfoWorks ICM Ultimate 2025/lib/ruby/vendor_ruby/2.4.0',
  'C:/Program Files/Autodesk/InfoWorks ICM Ultimate 2025/lib/ruby/site_ruby',
  'C:/Program Files/Autodesk/InfoWorks ICM Ultimate 2025/lib/ruby/site_ruby/2.4.0/x64-vcruntime140',
  'C:/Program Files/Autodesk/InfoWorks ICM Ultimate 2025/lib/ruby/site_ruby/2.4.0'
]



paths.each do |path|
  #Add path to load path
  $LOAD_PATH<< path
end

require 'json'

db=WSApplication.open "snumbat://localhost:40000/Databases/DynamoTest"
net = db.model_object_from_type_and_id("model network", 171)
open_net = net.open
tables = open_net.tables

def to_csharp_class_name(name)
  # Replace '2d' or '2D' at the beginning with 'TwoD'
  name1 = name.sub(/^(2d|2D)/, 'TwoD')
  name1 = name1.split('_').map(&:capitalize).join
  #Remove - from the name
  name1 = name1.gsub('-', '')
  #Remove any non-alphanumeric characters except underscores and replace it with underscores
  #name.gsub!(/[^a-zA-Z0-9_]/, '_')
  return name1
end

def to_csharp_field_name(ruby_field_name, ruby_class_name)
  if ruby_field_name == ruby_class_name
	  ruby_field_name = ruby_field_name + "Value"   	
  end

  # Replace '2d' or '2D' at the beginning with 'TwoD'
  name1 = ruby_field_name.sub(/^(2d|2D)/, 'TwoD')
  name1 = name1.split('_').map(&:capitalize).join
  #Remove - from the name
  name1 = name1.gsub('-', '')
  #Remove any non-alphanumeric characters except underscores and replace it with underscores
  #name.gsub!(/[^a-zA-Z0-9_]/, '_')
  return name1
end

ws_to_csharp_type = {
  "Flag"         => "string",
  "Boolean"      => "bool",
  "Single"       => "float",
  "Double"       => "double",
  "Short"        => "short",
  "Long"         => "long",
  "Date"         => "DateTime",
  "String"       => "string",
  "Array:Long"   => "List<long>",     # or "long[]"
  "Array:Double" => "List<double>",   # or "double[]"
  "WSStructure"  => "WSStructure",          # custom class representation
  "GUID"         => "string"
}
csharp_code = "// Auto-generated from tables.json\n\n"



# Generate struct definitions (WSStructure) in a separate file
structs_code = "// Auto-generated using Ruby\n\nnamespace WSClass.WSStructure\n{\n"
blobs = []
tables.each do |table|
  table.fields.each do |field|
    if field.data_type == "WSStructure"
      blobs << field
    end
  end
end
written_structs = []
blobs.each do |blob|
  # Skip if the struct has already been written
  if written_structs.include?(blob.name)
    next
  end
  class_name = to_csharp_class_name(blob.name)
  structs_code << "  public class #{class_name}\n  {\n"
  structs_code << "    public long RubyId { get; set; }\n"
  blob.fields.each do |field|
    prop_name = to_csharp_field_name(field.name, blob.name)
    structs_code << "    public #{ws_to_csharp_type[field.data_type]} #{prop_name} { get; set; }\n"
  end
  structs_code << "  }\n\n"
  written_structs << blob.name
end
structs_code << "}\n"
#File.write('C:/Users/lakeshc/OneDrive - Autodesk/011_Dynamo/000 - Repo/DynamoXICM/Plugin/Exchange/WSStructs.cs', structs_code)
File.write('WSStructure.cs', structs_code)

# Generate main table classes in a separate file
csharp_code = "// Auto-generated using Ruby\nusing WSClass.WSStructure; \nnamespace WSClass.WSRowObject\n{\n"
tables.each do |table|
  class_name = to_csharp_class_name(table.name)
  csharp_code << "  public class #{class_name}\n  {\n"
  csharp_code << "    public long RubyId { get; set; }\n"
  table.fields.each do |field|
    prop_name = to_csharp_field_name(field.name,table.name)
    if field.data_type == "WSStructure"
      csharp_code << "    public List<#{prop_name}> #{prop_name} { get; set; }\n"
    else
      csharp_code << "    public #{ws_to_csharp_type[field.data_type]} #{prop_name} { get; set; }\n"
    end
  end
  csharp_code << "  }\n\n"
end
csharp_code << "}\n"
#File.write('C:/Users/lakeshc/OneDrive - Autodesk/011_Dynamo/000 - Repo/DynamoXICM/Plugin/Exchange/WSOpenNetwork.cs', csharp_code)
File.write('WSRowObject.cs', csharp_code)
puts "WSStructure.cs and WSOpenNetwork.cs generated."




# 1. Ruby class names to C# class names
ruby_to_csharp_class = {}
# 2. C# class names to Ruby class names
csharp_to_ruby_class = {}
# 3. Ruby field names to C# field names (per class)
ruby_to_csharp_field = {}
# 4. C# field names to Ruby field names (per class)
csharp_to_ruby_field = {}

unprocessed_ws_structures = []
tables.each do |table|
  ruby_class = table.name
  csharp_class = to_csharp_class_name(table.name)
  ruby_to_csharp_class[ruby_class] = csharp_class
  csharp_to_ruby_class[csharp_class] = ruby_class

  ruby_to_csharp_field[ruby_class] = {}
  csharp_to_ruby_field[csharp_class] = {}

  table.fields.each do |field|
    if field.data_type == "WSStructure"
      # If the field is a WSStructure, we need to handle it separately
      unprocessed_ws_structures << field
    end
    ruby_field = field.name
    csharp_field = to_csharp_field_name(field.name,table.name)
    ruby_to_csharp_field[ruby_class][ruby_field] = csharp_field
    csharp_to_ruby_field[csharp_class][csharp_field] = ruby_field
  end
end

#Iterate through unprocessed WSStructures and add them to the mappings
unprocessed_ws_structures.each do |field|
  ruby_class = field.name
  csharp_class = to_csharp_class_name(field.name)
  ruby_to_csharp_class[ruby_class] = csharp_class
  csharp_to_ruby_class[csharp_class] = ruby_class

  ruby_to_csharp_field[ruby_class] = {}
  csharp_to_ruby_field[csharp_class] = {}

  field.fields.each do |sub_field|
    ruby_field = sub_field.name
    csharp_field = to_csharp_field_name(sub_field.name,field.name)
    ruby_to_csharp_field[ruby_class][ruby_field] = csharp_field
    csharp_to_ruby_field[csharp_class][csharp_field] = ruby_field
  end
end


current_dir = File.dirname(__FILE__)

File.write( 'ruby_to_csharp_class.json', JSON.pretty_generate(ruby_to_csharp_class))
File.write('csharp_to_ruby_class.json', JSON.pretty_generate(csharp_to_ruby_class))
File.write('ruby_to_csharp_field.json', JSON.pretty_generate(ruby_to_csharp_field))
File.write('csharp_to_ruby_field.json', JSON.pretty_generate(csharp_to_ruby_field))
