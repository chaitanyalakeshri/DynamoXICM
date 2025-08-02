def structure_to_hash(ws_structure, structure_name ,table_info_hash)
  fields = table_info_hash['fields']['suds_controls']['blobStructure']['fields'].keys
  ws_structure_hash = {}
  fields.each do |field|
    prop_name = to_csharp_field_name(field,structure_name)
    ws_structure_hash[prop_name] = ws_structure[field]
  end
  return ws_structure_hash
end