def open_model(model_id)
  begin
    net=$ICM_VARIABLES[model_id]
    open_net = net.open
    $ICM_VARIABLES[open_net.object_id.to_i] = open_net
    response = construct_response_hash_for_model_object(net)
    response['Data']['ObjectId'] = open_net.object_id
    response['DataType'] = 'WSModelObject'

    rescue => e
    response = error_response(e)
  end
  return response
end