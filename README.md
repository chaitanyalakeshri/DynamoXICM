# DynamoXICM

DynamoXICM is a powerful integration plugin designed to bridge Autodesk Dynamo with InfoWorks ICM (Integrated Catchment Modeling). This project enables users to automate, extend, and customize InfoWorks ICM workflows directly from Dynamo, leveraging both visual programming and advanced scripting.

---

## Table of Contents

1. [Overview](#overview)
2. [Key Features](#key-features)
3. [How It Works](#how-it-works)
4. [Architecture](#architecture)
5. [Usage Examples](#usage-examples)
6. [Project Structure](#project-structure)
7. [Setup & Installation](#setup--installation)
8. [Requirements](#requirements)
9. [Troubleshooting](#troubleshooting)
10. [License & Credits](#license--credits)

---

## Overview

**DynamoXICM** enables seamless, programmatic access to InfoWorks ICM’s core functionalities from Dynamo, allowing you to automate repetitive tasks, manage models, and integrate with external data sources. By exposing an API to create, modify, and query ICM databases, model groups, and model networks, DynamoXICM empowers engineers and researchers to design sophisticated hydrological workflows with minimal manual intervention.

---

## Key Features

- **Bidirectional Communication:** Establishes a robust communication channel between Dynamo and ICM using Windows named pipes, ensuring reliable data exchange.
- **Automated Model Operations:** Enables creation, management, and manipulation of ICM databases, model groups, and models from Dynamo scripts.
- **Scriptable Data Import/Export:** Supports importing model networks from CSV files and exporting results for further processing.
- **Batch Processing & Automation:** Facilitates large-scale, repeatable analyses and scenario management, reducing manual effort.
- **Error Handling & Logging:** Built-in logging and error propagation for easier debugging and workflow reliability.
- **Extensible Architecture:** Modular codebase allows easy extension for new commands, custom nodes, or integration with other software.

---

## How It Works

DynamoXICM operates as a Dynamo extension that launches an exchange server (via a batch file and Ruby scripts) and sets up a named pipe for inter-process communication with InfoWorks ICM.

**Typical Workflow:**

1. **Initialization:**  
   The plugin starts by launching an exchange process using a batch file (`run_exchange.bat`) and sets up a named pipe server (`ICMNamedPipeServer`).

2. **Function Execution:**  
   Dynamo scripts call high-level C# methods (e.g., `OpenDatabase`, `CreateModelGroup`), which package requests as JSON and send them through the named pipe.

3. **ICM Processing:**  
   On the other side, Ruby scripts (executed by the exchange process) receive requests, interact with InfoWorks ICM’s COM API, and generate responses.

4. **Results & Feedback:**  
   Responses are serialized as JSON, returned through the pipe, deserialized in Dynamo, and made available for further automation or user feedback.

---

## Architecture

```plaintext
+-------------+                          +------------------+       Named Pipes      +-----------------------+
| Dynamo User |  <-------------------->  | DynamoXICM Plugin |  <----------------->  | InfoWorks ICM Exchange|
+-------------+     (C# .NET, DLL)       +------------------+   (Ruby, Batch)        +---------------------+
        |                                                                              ^
        |---------------------- Dynamo Scripts/Nodes ----------------------------------|
```

- **C# Plugin:** Manages named pipes, launches exchange process, exposes API to Dynamo.
- **Exchange Process:** Batch file + Ruby scripts to interact with InfoWorks ICM.
- **Dynamo Scripts:** User-authored routines using provided API.

---

## Usage Examples

Below are some common use cases, illustrated in pseudo-code or Dynamo script style:

### 1. Open an ICM Database

```csharp
var db = Application.OpenDatabase("snumbat://localhost:40000/Databases/DynamoTest");
```

### 2. Create a Model Group

```csharp
var group = Database.CreateModelGroup("Scenario1", db);
```

### 3. Create a New ICM Model in a Group

```csharp
var model = Database.CreateICMModel("TestModel", group);
```

### 4. Import a Model Network from CSV

```csharp
var imported = ModelNetwork.ImportCsvModel("C:\\path\\to\\network.csv", model);
```

### 5. Batch Processing

You can automate the creation and manipulation of multiple models in a loop, leveraging Dynamo’s visual programming or C# scripting capabilities.

---

## Project Structure

```plaintext
/
├── Plugin/
│   ├── DynamoXICM.cs           # Main plugin logic (C#, named pipes, API)
│   ├── ExecuteFunction.cs      # Handles JSON serialization, function dispatch
│   └── Exchange/
│       ├── run_exchange.bat    # Starts the Ruby exchange server
│       └── *.rb                # Ruby scripts for direct ICM operations
├── ICMNodes/
│   ├── Application.cs          # API: Open/Create databases
│   ├── Database.cs             # API: Model groups, models
│   └── ModelNetwork.cs         # API: Import/Export model networks
├── TestProject/
│   └── Program.cs              # Example/test harness for the integration
├── README.md                   # This file
```

---

## Setup & Installation

1. **Build the Plugin:**
   - Open the solution in Visual Studio.
   - Build the `DynamoXICM` project (ensure dependencies are resolved).

2. **Deploy the DLL:**
   - Copy the built plugin DLL to the Dynamo extensions directory.

3. **Configure Exchange Scripts:**
   - Ensure `run_exchange.bat` and Ruby scripts are located as specified (`Plugin/Exchange/`).
   - Ensure `run_exchange.bat` points to the correct Exchange executable and script.

4. **Run Dynamo:**
   - Start Dynamo, and verify the DynamoXICM extension loads.
   - Setup ('Plugin/Exchange/run_exchange.bat')

---

## Requirements

- **Operating System:** Windows 11 (named pipes and InfoWorks ICM COM integration)
- **.NET:** Compatible with Dynamo extension requirements
- **Dynamo:** Autodesk Dynamo (version as per your environment)
- **InfoWorks ICM:** With scripting/COM API enabled
- **Ruby:** For exchange scripts (see specific Ruby version requirements)
- **Permissions:** Sufficient to execute external processes and scripts

---

## Troubleshooting

- **Platform Not Supported:**  
  Only Windows 11 is supported due to ICM COM integration and named pipes.

- **Pipe Connection Issues:**  
  Ensure no firewalls or security software block named pipes. Check that `run_exchange.bat` and Ruby scripts are executable.

- **ICM API Errors:**  
  Verify that InfoWorks ICM is installed, licensed, and scripting is enabled.

- **Debug Logging:**  
  Logging is provided by the plugin and can be extended for troubleshooting.

---

## License & Credits

_No explicit license is provided with this project. Please contact the repository owner for usage terms._

**Author:**  
[chaitanyalakeshri](https://github.com/chaitanyalakeshri)

---

> **DynamoXICM**: Automate and extend InfoWorks ICM with the power of Dynamo.