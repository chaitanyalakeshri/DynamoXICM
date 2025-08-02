def write_to_pipe(pipe_client, input)
  #Convert input hash to json
  input = input.to_json
  pipe_client.write("#{input}\0END\0")
  pipe_client.flush
end