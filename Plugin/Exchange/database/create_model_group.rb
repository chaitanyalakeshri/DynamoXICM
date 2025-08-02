#require util/error_response.rb

def create_model_group(name,database_object_id)
  begin
    db=$ICM_VARIABLES[database_object_id.to_i]
    #Create a new Model Group in root if it does not exist
    root_model_group_names = []
    db.root_model_objects.each do |mo|
        root_model_group_names.push(mo.name)
    end
    while root_model_group_names.include?(name)
      name = name + "!"
    end
    folder=db.new_model_object('Model group', name)
    $ICM_VARIABLES[folder.object_id] = folder
    return construct_response_hash_for_model_object(folder)
  rescue => e
    response = error_response(e)
    return response
  end
end

