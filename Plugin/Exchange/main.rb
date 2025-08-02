# current_directory = File.dirname(__FILE__)
# database_file= File.join(current_directory, 'database/open_database.rb')
paths = [
  'C:/Program Files/Autodesk/InfoWorks ICM Ultimate 2025/lib/ruby/2.4.0/x64-mswin64_140',
  'C:/Program Files/Autodesk/InfoWorks ICM Ultimate 2025/lib/ruby/2.4.0',
  'C:/Program Files/Autodesk/InfoWorks ICM Ultimate 2025/lib/ruby/vendor_ruby',
  'C:/Program Files/Autodesk/InfoWorks ICM Ultimate 2025/lib/ruby/vendor_ruby/2.4.0/x64-vcruntime140',
  'C:/Program Files/Autodesk/InfoWorks ICM Ultimate 2025/lib/ruby/vendor_ruby/2.4.0',
  'C:/Program Files/Autodesk/InfoWorks ICM Ultimate 2025/lib/ruby/site_ruby',
  'C:/Program Files/Autodesk/InfoWorks ICM Ultimate 2025/lib/ruby/site_ruby/2.4.0/x64-vcruntime140',
  'C:/Program Files/Autodesk/InfoWorks ICM Ultimate 2025/lib/ruby/site_ruby/2.4.0'
]


paths.each do |path|
  #Add path to load path
  $LOAD_PATH<< path
end

require_relative 'database/require.rb'
require_relative 'WSApplication/require.rb'
require_relative 'comms/require.rb'
require_relative 'WSOpenNetwork/require.rb'

pipe_client=connect_to_pipe()
variable_binding = binding
$ICM_VARIABLES = Hash.new

#$db = WSApplication.open 'C:\Users\lakeshc\OneDrive - Autodesk\001_Implementation Services\009 - Flood Risk Simulation\001 - Data\ICM database\Flood Risk Simulation.icmm'


while true
  response = read_from_pipe(pipe_client)
  break if response.downcase == "exit"

  puts "Evaluating: #{response}"
  begin
      result = evaluate_response(response)
      #puts "Result: #{result}"
  rescue => e
      result = "Error: #{e.message}"
      #puts result
  end
  puts "Result: #{result}"
  write_to_pipe(pipe_client, result)

end

pipe_client.close