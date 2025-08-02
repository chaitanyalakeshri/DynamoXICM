# def convert_network_field_name_from_ruby_to_csharp_name(name)
#   name1 = name.sub(/^(2d|2D)/, 'TwoD')
#   name1 = name1.split('_').map(&:capitalize).join
#   name1 = name1.gsub('-', '')
#   return name1
# end

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