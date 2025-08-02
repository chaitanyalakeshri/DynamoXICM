def construct_response_hash_for_model_object(mo)
  data = {
    "Name" => mo.name,
    "Path" => mo.path,
    "Id" => mo.id,
    "Type" => mo.type,
    "Comment" => mo.comment,
    "RubyId" => mo.object_id,
    }
    response = {
      "Data" => data,
      "DataType" => "WSModelObject"
    }
    return response

end