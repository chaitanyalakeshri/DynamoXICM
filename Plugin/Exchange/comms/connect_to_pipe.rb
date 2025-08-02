# Establishes a connection to a named pipe server.
#
# @param pipe_name [String] The name of the pipe to connect to. Defaults to '\\\\.\\pipe\\ICMNamedPipeServer'.
# @param debug [Boolean] Whether to print debug messages. Defaults to true.
# @return [File] A file object representing the connected pipe client.
# @raise [Errno::EAGAIN] If the connection cannot be established after multiple retries.
#
# The method attempts to connect to the specified named pipe server. If the connection
# fails due to the pipe being unavailable (Errno::EAGAIN), it retries up to 5 times,
# waiting 1 second between retries. If all retries fail, the exception is raised.
#
# Example:
#   pipe_client = connect_to_pipe
#   # Use the pipe_client to communicate with the pipe server.
def connect_to_pipe(pipe_name = '\\\\.\\pipe\\ICMNamedPipeServer', debug = true)
  if debug==true
    puts "Connecting to pipe at #{pipe_name}"
  end
  retries = 5
  begin
    #pipe_client = File.open('\\\\.\\pipe\\ICMNamedPipeServer', 'r+')
    pipe_client = File.open(pipe_name, 'r+')
  rescue Errno::EAGAIN
    if (retries -= 1) > 0
      sleep(1) # Wait for 1 second before retrying
      retry
    else
      raise
    end
  end
  if debug==true
    puts "Connected to pipe at #{pipe_name}"
  end
  return pipe_client
end

#Test
#connect_to_pipe()
#pipe_client = File.open('\\\\.\\pipe\\ICMNamedPipeServer', 'r+')
#puts "Connected to pipe"