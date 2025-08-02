def create_icm_model(model_name,model_folder_object_id)
  begin
    mo=$ICM_VARIABLES[model_folder_object_id.to_i]
    #Create a new Model in the specified Model Group
    #check if model with same name existing in the folder.
    #If yes, append '!' to the name
    #If no, create the model
    child_names = Array.new

    mo.children.each do |child|
      if child.type == 'Model Network'
        child_names.push(child.name)
      end
    end
    while child_names.include?(model_name)
      model_name = model_name + "!"
    end
    net = mo.new_model_object('Model Network', model_name)
    $ICM_VARIABLES[net.object_id] = net

    return construct_response_hash_for_model_object(net)

  rescue => e
    response = error_response(e)
  end
  return response
end


#Test
# db= WSApplication.open "snumbat://localhost:40000/Databases/DynamoTest"
# mo= db.model_object_from_type_and_id('Model group', 170)
# $ICM_VARIABLES = Hash.new
# $ICM_VARIABLES[mo.object_id.to_s] = mo
# obj_id= mo.object_id.to_s
# a = create_icm_model('Test Model Network', obj_id)
