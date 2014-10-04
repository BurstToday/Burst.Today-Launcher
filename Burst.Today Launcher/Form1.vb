Public Class Form1

    Dim WithEvents wc As System.Net.WebClient
    Dim WithEvents wc2 As System.Net.WebClient
    Dim WithEvents wc3 As System.Net.WebClient
    Dim WalletDL As Integer = 0
    Dim MinerDL As Integer = 0
    Dim BurstTodayDL As Integer = 0

    

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToScreen()
        Button1.PerformClick()
    End Sub

    'ProgressBar for Wallet
    Private Sub DownloadProgress(ByVal sender As Object, ByVal e As System.Net.DownloadProgressChangedEventArgs) Handles wc.DownloadProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
        ProgressBar1.Refresh()
        Me.Refresh()
    End Sub

    'Progressbar for Miner
    Private Sub DownloadProgress2(ByVal sender As Object, ByVal e As System.Net.DownloadProgressChangedEventArgs) Handles wc2.DownloadProgressChanged
        ProgressBar2.Value = e.ProgressPercentage
        ProgressBar2.Refresh()
        Me.Refresh()
    End Sub

    'Progressbar for Burst.Today
    Private Sub DownloadProgress3(ByVal sender As Object, ByVal e As System.Net.DownloadProgressChangedEventArgs) Handles wc3.DownloadProgressChanged
        ProgressBar3.Value = e.ProgressPercentage
        ProgressBar3.Refresh()
        Me.Refresh()
    End Sub

    'Wallet Download Complete
    Private Sub wc_DownloadFileCompleted(sender As Object, e As System.ComponentModel.AsyncCompletedEventArgs) Handles wc.DownloadFileCompleted
        Try
            If e.Cancelled Then

            ElseIf e.Error IsNot Nothing Then
                MessageBox.Show(e.Error.ToString(), "Download Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                'Extract the wallet
                Using Zippo As Ionic.Zip.ZipFile = Ionic.Zip.ZipFile.Read("C:\Burst.Today\Wallet.zip")
                    System.Threading.Thread.Sleep(100)
                    Zippo.ExtractAll("C:\Burst.Today\Wallet\", Ionic.Zip.ExtractExistingFileAction.OverwriteSilently)
                    ProgressBar1.Value = 110
                    WalletDL = 1
                    System.Threading.Thread.Sleep(500)
                    ProgressBar1.ForeColor = Color.Green
                    ProgressBar1.Refresh()
                    If MinerDL = 1 And BurstTodayDL = 1 Then
                        'MsgBox("1")
                        StartBurstToday()
                    End If
                End Using
            End If
        Catch ex As Exception
            ProgressBar1.ForeColor = Color.Red
            ProgressBar1.Refresh()
        End Try
    End Sub

    'Miner Download Complete
    Private Sub wc2_DownloadFileCompleted(sender As Object, e As System.ComponentModel.AsyncCompletedEventArgs) Handles wc2.DownloadFileCompleted
        Try
            If e.Cancelled Then

            ElseIf e.Error IsNot Nothing Then
                MessageBox.Show(e.Error.ToString(), "Download Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                ' wc2.Dispose()
                'Extract the Miner
                Using Zippo As Ionic.Zip.ZipFile = Ionic.Zip.ZipFile.Read("C:\Burst.Today\Miner.zip")
                    System.Threading.Thread.Sleep(100)
                    Zippo.ExtractAll("C:\Burst.Today\Miner\", Ionic.Zip.ExtractExistingFileAction.OverwriteSilently)
                    ProgressBar2.Value = 110
                    MinerDL = 1
                    System.Threading.Thread.Sleep(500)
                    ProgressBar2.ForeColor = Color.DarkGreen
                    ProgressBar2.Refresh()
                    If WalletDL = 1 And BurstTodayDL = 1 Then
                        'MsgBox("2")
                        StartBurstToday()
                    End If
                End Using
            End If
        Catch ex As Exception
            ProgressBar2.ForeColor = Color.Red
            ProgressBar2.Refresh()


        End Try
    End Sub

    'Burst.Today Download Complete
    Private Sub wc3_DownloadFileCompleted(sender As Object, e As System.ComponentModel.AsyncCompletedEventArgs) Handles wc3.DownloadFileCompleted
        Try
            If e.Cancelled Then

            ElseIf e.Error IsNot Nothing Then
                MessageBox.Show(e.Error.ToString(), "Download Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                ' wc3.Dispose()
                'Extract the Miner
                Using Zippo As Ionic.Zip.ZipFile = Ionic.Zip.ZipFile.Read("C:\Burst.Today\Burst.Today.zip")
                    System.Threading.Thread.Sleep(100)
                    Zippo.ExtractAll("C:\Burst.Today\Burst.Today\", Ionic.Zip.ExtractExistingFileAction.OverwriteSilently)
                    ProgressBar3.Value = 110
                    BurstTodayDL = 1
                    System.Threading.Thread.Sleep(500)
                    ProgressBar3.ForeColor = Color.DarkGreen
                    ProgressBar3.Refresh()
                    If WalletDL = 1 And MinerDL = 1 Then
                        'MsgBox("3")
                        StartBurstToday()
                    End If
                End Using
            End If
        Catch ex As Exception
            ProgressBar3.ForeColor = Color.Red
            ProgressBar3.Refresh()
        End Try
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Cursor = Cursors.WaitCursor

        'Create Folders for download
        'C:\Burst.Today
        If My.Computer.FileSystem.DirectoryExists("C:\Burst.Today") Then
            'Do Nothing, the folder Exists
        Else
            My.Computer.FileSystem.CreateDirectory("C:\Burst.Today")
        End If

        'C:\Burst.Today\Wallet\
        If My.Computer.FileSystem.DirectoryExists("C:\Burst.Today\Wallet") Then
            'Do Nothing, the folder Exists
        Else
            My.Computer.FileSystem.CreateDirectory("C:\Burst.Today\Wallet")
        End If

        'C:\Burst.Today\Miner\
        If My.Computer.FileSystem.DirectoryExists("C:\Burst.Today\Miner") Then
            'Do Nothing, the folder Exists
        Else
            My.Computer.FileSystem.CreateDirectory("C:\Burst.Today\Miner")
        End If

        'C:\Burst.Today\Burst.today\
        If My.Computer.FileSystem.DirectoryExists("C:\Burst.Today\Burst.Today") Then
            'Do Nothing, the folder Exists
        Else
            My.Computer.FileSystem.CreateDirectory("C:\Burst.Today\Burst.Today")
        End If

        'Wallet
        Dim URL As String = "https://github.com/BurstProject/burstcoin/archive/master.zip"
        Dim Path As String = "C:\Burst.Today\Wallet.zip"
        wc = New System.Net.WebClient
        wc.DownloadFileAsync(New Uri(URL), Path)
        'Dim WalletTask As System.Threading.Tasks.Task = New Task(Of String)(Function()
        'wc.DownloadFileAsync(New Uri(URL), Path)
        'Return 1
        'End Function)

        'Miner
        Dim URL2 As String
        'Ugh I hate doing this but the pool comes from a random fucking ZIP on the internet until the douche developer implements pools
        Dim WeAreBeingReallyStupid As Boolean = False
        If WeAreBeingReallyStupid = True Then
            'We are being really Stupid
            URL2 = "https://s3-ap-southeast-1.amazonaws.com/burst-mirror/burst-pool-miner-r2.zip"
        Else
            'Hopefully someday we stop being stupid
            URL2 = "https://github.com/BurstProject/pocminer/archive/master.zip"
        End If




        Dim Path2 As String = "C:\Burst.Today\Miner.zip"
        wc2 = New System.Net.WebClient
        wc2.DownloadFileAsync(New Uri(URL2), Path2)
        'Dim MinerTask As System.Threading.Tasks.Task = New Task(Of String)(Function()
        'wc2.DownloadFileAsync(New Uri(URL2), Path2)
        'Return 1
        'End Function)

        'Burst.Today
        Dim URL3 As String = "https://github.com/BurstToday/BurstTodayUI/archive/master.zip"
        Dim Path3 As String = "C:\Burst.Today\Burst.Today.zip"
        wc3 = New System.Net.WebClient
        wc3.DownloadFileAsync(New Uri(URL3), Path3)

    End Sub

    Private Sub StartBurstToday()
        System.Threading.Thread.Sleep(1000)

        '--------------PATCH THE BATCH FILES---------------
        ' Make a reference to a directory.
        Dim di As New System.IO.DirectoryInfo("C:\Burst.Today\Miner\pocminer-master")
        ' Get a reference to each file in that directory.
        Dim fiArr As System.IO.FileInfo() = di.GetFiles()
        ' Display the names of the files.
        Dim fri As System.IO.FileInfo
        For Each fri In fiArr

            If fri.Name.EndsWith(".bat") Then
                Dim Reader() As String = System.IO.File.ReadAllLines(fri.FullName)
                Dim Readersize As Integer = Reader.Length
                Dim looper As Integer = 0
                While looper < Readersize
                    'go through each of the files and do a replace 
                    Reader(looper) = Reader(looper).Replace("java -", "C:\Windows\SysWOW64\java -")
                    'Reader(looper) = Reader(looper).Replace("Xmx4000m -", "Xmx" & TextBox4.Text & "m -")
                    looper = looper + 1
                End While
                ' MsgBox("File =" & fri.Name)
                System.IO.File.WriteAllLines(fri.FullName, Reader)
            End If

            Console.WriteLine(fri.Name)
        Next fri
        '--------------END PATCH THE BATCH FILES---------------


      
        Dim compiler As New Process()
        compiler.StartInfo.FileName = "C:\Burst.Today\Burst.Today\BurstTodayUI-master\Burst\bin\Debug\burst.exe"
        compiler.StartInfo.UseShellExecute = False
        compiler.Start()
        Application.Exit()


    End Sub


End Class
