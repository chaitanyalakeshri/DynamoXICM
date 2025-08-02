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

require_relative 'database/require.rb'
require_relative 'WSApplication/require.rb'
require_relative 'comms/require.rb'
require_relative 'WSOpenNetwork/require.rb'



# pipe_client=connect_to_pipe()
# variable_binding = binding
# $ICM_VARIABLES = Hash.new

# db = WSApplication.open "snumbat://localhost:40000/Databases/DynamoTest"
# mo = db.model_object_from_type_and_id("Model group", 171)
# create_model_group("Test Model Group", mo.object_id.to_s)
# # open_net = net.open
# # open_net.csv_import('C:\Users\lakeshc\OneDrive - Autodesk\011_Dynamo\003_Graph\002 - Create Network\Existing ICM Network.csv', {})


db=WSApplication.open "snumbat://localhost:40000/Databases/DynamoTest"
net = db.model_object_from_type_and_id("model network", 171)
open_net = net.open
#ro= open_net.row_object('hw_subcatchment', '10.002!')
$ICM_VARIABLES = Hash.new
$ICM_VARIABLES[open_net.object_id.to_i] = open_net

a = get_row_object('1_S', open_net.object_id, 'hw_node')

puts a
# def convert_network_field_name_from_ruby_to_csharp_name(name)
#   # Replace '2d' or '2D' at the beginning with 'TwoD'
#   name1 = name.sub(/^(2d|2D)/, 'TwoD')
#   name1 = name1.split('_').map(&:capitalize).join
#   #Remove - from the name
#   name1 = name1.gsub('-', '')
#   #Remove any non-alphanumeric characters except underscores and replace it with underscores
#   #name.gsub!(/[^a-zA-Z0-9_]/, '_')
#   return name1
# end

# def ws_structure_to_hash(ws_structure, table_info_hash)
#   fields = table_info_hash['fields']['suds_controls']['blobStructure']['fields'].keys
#   ws_structure_hash = {}
#   fields.each do |field|
#     prop_name = convert_network_field_name_from_ruby_to_csharp_name(field)
#     ws_structure_hash[prop_name] = ws_structure[field]
#   end
#   return ws_structure_hash
# end

# def construct_response_hash_for_row_object(mo)
#   table_info_hash = JSON.parse(mo.table_info.tableinfo_json)
#   data = {"RubyId" => mo.object_id}
#   mo.table_info.fields.each do |field|
#     if field.data_type == "WSStructure"
#       ws_structure_array = []
#       mo[field.name].each do |sub_mo|
#         ws_structure_array << ws_structure_to_hash(sub_mo,table_info_hash)
#       end
#       data[field.name] = ws_structure_array
#     else
#       data[field.name] = mo[field.name]
#     end
#   end

#   response = {
#     "Data" => data,
#     "DataType" => "WSModelObject"
#   }

#   return response
# end

# #File.write('C:\Users\lakeshc\OneDrive - Autodesk\011_Dynamo\000 - Repo\DynamoXICM\Plugin\Exchange\tables.json', construct_response_hash_for_row_object(ro).to_json)

# #write json to file
# require 'json'
# json_string = ro.table_info.tableinfo_json
# a = JSON.parse(ro.table_info.tableinfo_json)
# File.write('C:\Users\lakeshc\OneDrive - Autodesk\011_Dynamo\000 - Repo\DynamoXICM\Plugin\Exchange\tables.json', a)
# puts a['fields']['suds_controls'] 