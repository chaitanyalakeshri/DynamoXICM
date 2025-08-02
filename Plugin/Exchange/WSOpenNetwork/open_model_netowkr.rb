def open_model_network(model_ruby_id)
  begin
    net = $ICM_VARIABLES[model_ruby_id.to_i]
    open_net = net.open
    $ICM_VARIABLES[open_net.object_id.to_i] = open_net
    response = {
      "Data" => {"RubyId" => open_net.object_id , "ParentObjectRubyId" => net.object_id},
      "DataType" => "WSOpenNetwork"
    }
  rescue => e
    response = error_response(e)
  end
  return response
end

