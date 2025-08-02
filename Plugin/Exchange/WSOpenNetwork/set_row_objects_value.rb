def set_row_objects_value(ro,field,value)
  begin
    ro[field] = value
    ro.write
    response = construct_response_hash_for_row_object(ro)
    return response
  rescue=> e
    response = error_response(e)
  end
end


# db=WSApplication.open "snumbat://localhost:40000/Databases/DynamoTest"
# net = db.model_object_from_type_and_id("model network", 171)
# open_net = net.open
# ro= open_net.row_object('hw_subcatchment', '10.002!')
# puts ro['system_type']
# open_net.transaction_begin
# SetRowObjectsValue(ro,'system_type','foul')
# open_net.transaction_commit
# puts ro['system_type']