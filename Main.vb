Imports System
Imports Microsoft.Win32
Imports System.Text
Imports System.io
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings


Module Main
    Public frmFirmas As New frmFirma
    Public form3 As New frmS03
    Public form4 As New frmS04

    Public Function Pad(ByVal stOrigen As String, ByVal stChar As String, ByVal inLongitud As Integer, ByVal inJustificado As Integer) As String
        Dim inIndex As Integer
        Dim stResultado As String

        stResultado = ""

        For inIndex = 1 To inLongitud - Len(stOrigen)
            stResultado = stResultado & stChar
        Next inIndex

        Select Case inJustificado
            Case PAD_RIGHT
                Return (stResultado & stOrigen)
            Case PAD_LEFT
                Return (stOrigen & stResultado)
            Case Else
                Return stOrigen
        End Select
    End Function

  
    Sub Main()
        System.Windows.Forms.Application.Run(frmFirmas)
    End Sub
End Module
