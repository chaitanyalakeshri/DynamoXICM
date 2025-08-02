def get_row_object(id, on_ruby_id, table_name)
  on = $ICM_VARIABLES[on_ruby_id.to_i]
  ro = on.row_object(table_name, id.to_s)
  $ICM_VARIABLES[ro.object_id.to_i] = ro.object_id
  response = construct_response_hash_for_row_object(ro)
  begin
    response = construct_response_hash_for_row_object(ro)
    return response
  rescue=> e
    response = error_response(e)
  end
end