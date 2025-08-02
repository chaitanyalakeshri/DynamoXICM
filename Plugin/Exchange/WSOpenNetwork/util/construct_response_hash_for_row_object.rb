require 'json'
def construct_response_hash_for_row_object(mo)
  table_info_hash = JSON.parse(mo.table_info.tableinfo_json)
  #puts "Table Info Hash: #{table_info_hash}"
  data = {"RubyId" => mo.object_id}
  mo.table_info.fields.each do |field|
    if field.data_type == "WSStructure"
      ws_structure_array = []
      mo[field.name].each do |sub_mo|
        ws_structure_array << structure_to_hash(sub_mo,field.name,table_info_hash)
      end
      data[to_csharp_field_name(field.name, table_info_hash["tableName"]) ] = ws_structure_array
    else
      data[to_csharp_field_name(field.name, table_info_hash["tableName"])] = mo[field.name]
    end
  end
  data["RubyId"] = mo.object_id
  response = {
    "Data" => data,
    "DataType" => "#{to_csharp_class_name(mo.table_info.name)}"
  }
  return response
end