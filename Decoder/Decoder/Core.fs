namespace Decoder

open System
open System.IO
open System.Diagnostics
open System.Text

module Core =

    // Use the provided folder path
    let exeFolderPath = @"C:\Temp\Anton" // Use literal string with @ to avoid escaping backslashes
    let exeFileName = "encoder-windows-amd64.exe"
    let batFileName = "-a.bat"

    //let callExternalFunction (b: byte) (seed: int) =
    //    // Convert byte to hex string
    //    let hexString = $"{b:X2}"
    //    // Create file names for input and output
    //    let inputFile = $"{hexString}.txt"
    //    let outputFile = $"{hexString}-{seed}.res"

    //    // Ensure the folder exists
    //    Directory.CreateDirectory(exeFolderPath) |> ignore

    //    // Write the byte three times to the input file in the specified folder
    //    use fileStream = new FileStream(Path.Combine(exeFolderPath, inputFile), FileMode.Create, FileAccess.Write)
    //    let bytesToWrite = Array.init 3 (fun _ -> b)
    //    fileStream.Write(bytesToWrite, 0, bytesToWrite.Length)

    //    // Construct the command to execute using cmd
    //    let command = $"/C cd {exeFolderPath} && {exeFileName} {seed} {inputFile} {outputFile}"

    //    // Prepare the process start info
    //    let startInfo = ProcessStartInfo()
    //    startInfo.FileName <- "cmd.exe"
    //    startInfo.Arguments <- command
    //    startInfo.UseShellExecute <- false

    //    // Start the process
    //    use p = new Process()
    //    p.StartInfo <- startInfo
    //    p.Start() |> ignore
    //    p.WaitForExit()

    //    // Read the output file
    //    use fileStream = new FileStream(Path.Combine(exeFolderPath, outputFile), FileMode.Open, FileAccess.Read)
    //    let bytesRead = Array.zeroCreate<byte> 4
    //    fileStream.Read(bytesRead, 0, 4) |> ignore

    //    // Return the read bytes
    //    bytesRead


    let generateBatchFileAndInputs (minSeed: int) (maxSeed: int) =
        // Generate the batch file content
        let batContent = StringBuilder()
        let batFilePath = Path.Combine(exeFolderPath, batFileName)

        // Ensure the folder exists
        Directory.CreateDirectory(exeFolderPath) |> ignore

        for b in 0uy .. 255uy do
            printfn $"Running for b: '{b}'."

            // Write the input file for each byte value
            let hexString = $"{b:X2}"
            let inputFile = Path.Combine(exeFolderPath, $"{hexString}.txt")
            use fileStream = new FileStream(inputFile, FileMode.Create, FileAccess.Write)
            let bytesToWrite = Array.init 3 (fun _ -> b)
            fileStream.Write(bytesToWrite, 0, bytesToWrite.Length)

            for seed in minSeed .. maxSeed do
                let outputFile = $"{hexString}-{seed}.res"
                let commandLine = $"{exeFileName} {seed} {Path.GetFileName(inputFile)} {outputFile}"
                batContent.AppendLine(commandLine)
                |> ignore

        // Write the batch file content to the file in exeFolderPath
        File.WriteAllText(batFilePath, batContent.ToString())


    //// Define a record to hold the byte, seed, and result
    //type ByteSeedResult = { Byte: byte; Seed: int; Result: byte[] }


    //let callAllFunctions (minSeed: int) (maxSeed: int) =
    //    let results = ResizeArray<ByteSeedResult>() // Using ResizeArray for dynamic addition

    //    // Loop over each byte value
    //    for b in 0uy .. 255uy do
    //        // Loop over each seed value
    //        for seed in minSeed .. maxSeed do
    //            // Call the external function
    //            let result = callExternalFunction b seed
    //            // Create a record and add it to the results
    //            results.Add({ Byte = b; Seed = seed; Result = result })
    
    //    // Convert ResizeArray to array before returning
    //    results.ToArray()


    //let outputResultsWithHeader (results: ByteSeedResult[]) =
    //    // Print the header
    //    printfn "Byte,Seed,b1,b2,b3,b4"

    //    // Iterate over each result and print the values in a comma-separated row.
    //    results
    //    |> Array.iter (fun result ->
    //        let byteStr = $"{result.Byte:X2}"
    //        let seedStr = $"{result.Seed}"
    //        let resultBytes = result.Result |> Array.map (fun b -> $"{b:X2}")
    //        let resultStr = String.Join(",", resultBytes)
    //        printfn $"{byteStr},{seedStr},{resultStr}"
    //    )


    //let runAll minSeed maxSeed =
    //    let r = callAllFunctions minSeed maxSeed
    //    outputResultsWithHeader r

    // Define the record type to hold byte, seed, and result
    type ByteSeedResult = { Byte: byte; Seed: int; Result: byte[] }

    // Function to read the results and return an array of ByteSeedResult
    let readResults (minSeed: int) (maxSeed: int) =
        let results = ResizeArray<ByteSeedResult>()

        for b in 0uy .. 255uy do
            let hexString = $"{b:X2}"
            for seed in minSeed .. maxSeed do
                let outputFile = Path.Combine(exeFolderPath, $"{hexString}-{seed}.res")
                // Ensure the output file exists before attempting to read
                if File.Exists(outputFile) then
                    use fileStream = new FileStream(outputFile, FileMode.Open, FileAccess.Read)
                    let bytesRead = Array.zeroCreate<byte> 4
                    let readCount = fileStream.Read(bytesRead, 0, bytesRead.Length)
                    if readCount = 4 then // Ensure that we have read exactly 4 bytes
                        results.Add({ Byte = b; Seed = seed; Result = bytesRead })

        results.ToArray()


    // Function to print the results with a header
    let outputResultsWithHeader (results: ByteSeedResult[]) =
        // Print the header
        printfn "Byte,Seed,bb,b2,b3,b4"

        // Iterate over each result and print the values in a comma-separated row
        results
        |> Array.iter (fun result ->
            let byteStr = $"\"{result.Byte:X2}\""
            let seedStr = $"{result.Seed}"
            let resultBytes = result.Result |> Array.map (fun b -> $"\"{b:X2}\"")
            let resultStr = String.Join(",", resultBytes)
            printfn $"{byteStr},{seedStr},{resultStr}"
        )


    let outputResultsToCSV (results: ByteSeedResult[]) =
        let csvFilePath = Path.Combine(exeFolderPath, "-b.csv")
        // Open the file for writing
        use writer = new StreamWriter(csvFilePath)
    
        // Write the header
        writer.WriteLine("Byte,Seed,bb,b2,b3,b4")

        // Iterate over each result and write the values in a comma-separated row
        results
        |> Array.iter (fun result ->
            let byteStr = $"\"{result.Byte:X2}\""
            let seedStr = $"{result.Seed}"
            let resultBytes = result.Result |> Array.map (fun b -> $"\"{b:X2}\"")
            let resultStr = String.Join(",", resultBytes)
            writer.WriteLine($"{byteStr},{seedStr},{resultStr}")
        )
