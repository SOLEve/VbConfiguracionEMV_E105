Imports System.Security.Cryptography

Public NotInheritable Class TripleDES
    Public Shared userKey As Byte()
    Public Shared isEmpty As Boolean = True
    Private TripleDESCSP As New TripleDESCryptoServiceProvider

    Sub New(ByVal cipherMode As Integer)
        'Dim utf8Encoder As System.Text.UTF8Encoding = New System.Text.UTF8Encoding
        'Dim keyBytes() As Byte = utf8Encoder.GetBytes(key)
        TripleDESCSP.Mode = cipherMode
        TripleDESCSP.Key = userKey
        TripleDESCSP.Padding = PaddingMode.None
        TripleDESCSP.IV = {&H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0}
    End Sub

    Public Function EncryptDataToMemory(ByVal plainTextBytes As Byte()) As Byte()
        Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream
        Dim encStream As New CryptoStream(ms, TripleDESCSP.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write)

        encStream.Write(plainTextBytes, 0, plainTextBytes.Length)
        encStream.FlushFinalBlock()
        ms.Position = 0
        Dim encryptedBytes(ms.Length - 1) As Byte
        ms.Read(encryptedBytes, 0, ms.Length)
        ms.Close()

        Return encryptedBytes
    End Function

    Public Function DecryptDataFromMemory(ByVal encryptedBytes As Byte()) As Byte()
        Dim ms As New System.IO.MemoryStream
        Dim decStream As New CryptoStream(ms, TripleDESCSP.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write)

        decStream.Write(encryptedBytes, 0, encryptedBytes.Length)
        decStream.FlushFinalBlock()
        ms.Position = 0
        Dim plainTextBytes(ms.Length - 1) As Byte
        ms.Read(plainTextBytes, 0, ms.Length)
        ms.Close()

        Return plainTextBytes
    End Function

    Public Function EncryptDataToMemory(ByVal plainText As String) As Byte()
        Dim utf8Encoder As System.Text.UTF8Encoding = New System.Text.UTF8Encoding
        Dim plainTextBytes() As Byte = utf8Encoder.GetBytes(plainText)
        Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream
        Dim encStream As New CryptoStream(ms, TripleDESCSP.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write)

        encStream.Write(plainTextBytes, 0, plainTextBytes.Length)
        encStream.FlushFinalBlock()
        ms.Position = 0
        Dim encryptedBytes(ms.Length - 1) As Byte
        ms.Read(encryptedBytes, 0, ms.Length)
        ms.Close()

        Return encryptedBytes
    End Function

    Public Function DecryptDataFromMemory(ByVal encryptedText As String) As String
        Dim utf8Encoder As New System.Text.UTF8Encoding
        Dim encryptedTextBytes() As Byte = utf8Encoder.GetBytes(encryptedText)
        Dim ms As New System.IO.MemoryStream
        Dim decStream As New CryptoStream(ms, TripleDESCSP.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write)

        decStream.Write(encryptedTextBytes, 0, encryptedTextBytes.Length)
        decStream.FlushFinalBlock()
        ms.Position = 0
        Dim plainTextBytes(ms.Length - 1) As Byte
        ms.Read(plainTextBytes, 0, ms.Length)
        ms.Close()

        Return utf8Encoder.GetString(plainTextBytes)
    End Function

End Class
