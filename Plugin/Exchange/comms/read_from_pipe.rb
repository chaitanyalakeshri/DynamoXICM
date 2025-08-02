def read_from_pipe(pipe_client)
  buffer = ""
  delimiter = "\0END\0"
  while true
    char = pipe_client.getc
    buffer << char
    break if buffer.end_with?(delimiter)
  end
  response = buffer.chomp(delimiter)
  return response
end