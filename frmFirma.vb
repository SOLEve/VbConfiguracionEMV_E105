Imports System
Imports System.IO
Imports Microsoft.Win32
Imports System.Text
Imports System.Threading
Imports System.Security.Cryptography
Imports System.Xml

Public Class frmFirma
    Inherits System.Windows.Forms.Form
    Private m_ModemPort As Integer = 0
    Private bReadRS232 As Boolean
    Private inBaudRate As Integer
    Private inDataBit As Integer
    Private inDataParity As Integer
    Private inDataStopBit As Integer
    Private inTimeOutRecep As Integer
    Private inBufferSize As Integer
    Private m_CommPort As New Rs232
    Private szMensaje As String
    Private inRecibioACK As Integer
    Private boManualStop As Boolean = False
    Private boGB3OrigEnabled As Boolean = False
    Private boTSOrigEnabled As Boolean = False
    Private boCBDOrigEnabled As Boolean = False
    Friend WithEvents ButtonCargarXml As System.Windows.Forms.Button
    Friend WithEvents ButtonLimpiarAids As System.Windows.Forms.Button
    Friend WithEvents ButtonLimpiarCAPK As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Public receiveThread As Thread = Nothing
    Delegate Sub LblStatusFocusCallback()
    Delegate Sub SetLblStatusReadOnlyCallback(ByVal readOnlyFlag As Boolean)
    Delegate Sub SetLblStatusTextCallback(ByVal text As String)
    Delegate Sub SetLblStatusSelectedTextCallback(ByVal text As String)
    Delegate Sub LblStatusScrollToCaretCallback()
    Delegate Sub LblStatusRefreshCallback()

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        szMensaje = ""
        inRecibioACK = 0
        'CargarTransacciones()
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents LabelStatus As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnEnviarComando As System.Windows.Forms.Button
    Friend WithEvents cmbPuerto As System.Windows.Forms.ComboBox
    Friend WithEvents txtAreaResultados As System.Windows.Forms.TextBox
    Friend WithEvents btnLimpiarStatus As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFirma))
        Me.LabelStatus = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnEnviarComando = New System.Windows.Forms.Button()
        Me.cmbPuerto = New System.Windows.Forms.ComboBox()
        Me.txtAreaResultados = New System.Windows.Forms.TextBox()
        Me.btnLimpiarStatus = New System.Windows.Forms.Button()
        Me.ButtonCargarXml = New System.Windows.Forms.Button()
        Me.ButtonLimpiarAids = New System.Windows.Forms.Button()
        Me.ButtonLimpiarCAPK = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'LabelStatus
        '
        Me.LabelStatus.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.LabelStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelStatus.ForeColor = System.Drawing.Color.Navy
        Me.LabelStatus.Location = New System.Drawing.Point(20, 40)
        Me.LabelStatus.Name = "LabelStatus"
        Me.LabelStatus.Size = New System.Drawing.Size(233, 20)
        Me.LabelStatus.TabIndex = 2
        Me.LabelStatus.Text = "Estatus de Procesamiento:"
        '
        'btnEnviarComando
        '
        Me.btnEnviarComando.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnEnviarComando.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.btnEnviarComando.Location = New System.Drawing.Point(638, 166)
        Me.btnEnviarComando.Name = "btnEnviarComando"
        Me.btnEnviarComando.Size = New System.Drawing.Size(144, 44)
        Me.btnEnviarComando.TabIndex = 178
        Me.btnEnviarComando.Text = "Aplicar Configuracion"
        Me.btnEnviarComando.UseVisualStyleBackColor = False
        '
        'cmbPuerto
        '
        Me.cmbPuerto.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.cmbPuerto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPuerto.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32"})
        Me.cmbPuerto.Location = New System.Drawing.Point(727, 91)
        Me.cmbPuerto.Name = "cmbPuerto"
        Me.cmbPuerto.Size = New System.Drawing.Size(144, 24)
        Me.cmbPuerto.TabIndex = 185
        '
        'txtAreaResultados
        '
        Me.txtAreaResultados.AcceptsReturn = True
        Me.txtAreaResultados.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.txtAreaResultados.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtAreaResultados.Font = New System.Drawing.Font("Lucida Console", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAreaResultados.Location = New System.Drawing.Point(24, 63)
        Me.txtAreaResultados.Multiline = True
        Me.txtAreaResultados.Name = "txtAreaResultados"
        Me.txtAreaResultados.ReadOnly = True
        Me.txtAreaResultados.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtAreaResultados.Size = New System.Drawing.Size(583, 516)
        Me.txtAreaResultados.TabIndex = 194
        '
        'btnLimpiarStatus
        '
        Me.btnLimpiarStatus.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnLimpiarStatus.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnLimpiarStatus.Location = New System.Drawing.Point(213, 586)
        Me.btnLimpiarStatus.Name = "btnLimpiarStatus"
        Me.btnLimpiarStatus.Size = New System.Drawing.Size(176, 41)
        Me.btnLimpiarStatus.TabIndex = 195
        Me.btnLimpiarStatus.Text = "Limpiar"
        Me.btnLimpiarStatus.UseVisualStyleBackColor = False
        '
        'ButtonCargarXml
        '
        Me.ButtonCargarXml.Location = New System.Drawing.Point(820, 166)
        Me.ButtonCargarXml.Name = "ButtonCargarXml"
        Me.ButtonCargarXml.Size = New System.Drawing.Size(144, 44)
        Me.ButtonCargarXml.TabIndex = 196
        Me.ButtonCargarXml.Text = "Cargar Archivo XML"
        Me.ButtonCargarXml.UseVisualStyleBackColor = True
        '
        'ButtonLimpiarAids
        '
        Me.ButtonLimpiarAids.Location = New System.Drawing.Point(643, 283)
        Me.ButtonLimpiarAids.Name = "ButtonLimpiarAids"
        Me.ButtonLimpiarAids.Size = New System.Drawing.Size(144, 44)
        Me.ButtonLimpiarAids.TabIndex = 197
        Me.ButtonLimpiarAids.Text = "Limpiar Aid's"
        Me.ButtonLimpiarAids.UseVisualStyleBackColor = True
        '
        'ButtonLimpiarCAPK
        '
        Me.ButtonLimpiarCAPK.Location = New System.Drawing.Point(820, 283)
        Me.ButtonLimpiarCAPK.Name = "ButtonLimpiarCAPK"
        Me.ButtonLimpiarCAPK.Size = New System.Drawing.Size(144, 44)
        Me.ButtonLimpiarCAPK.TabIndex = 198
        Me.ButtonLimpiarCAPK.Text = "Limpiar Llaves Públicas"
        Me.ButtonLimpiarCAPK.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(643, 403)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(144, 44)
        Me.Button4.TabIndex = 199
        Me.Button4.Text = "Ayuda"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Navy
        Me.Label1.Location = New System.Drawing.Point(758, 65)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 23)
        Me.Label1.TabIndex = 200
        Me.Label1.Text = "PUERTO"
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(820, 403)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(144, 44)
        Me.Button5.TabIndex = 201
        Me.Button5.Text = "Creditos"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'frmFirma
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.SystemColors.Menu
        Me.ClientSize = New System.Drawing.Size(983, 645)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.ButtonLimpiarCAPK)
        Me.Controls.Add(Me.ButtonLimpiarAids)
        Me.Controls.Add(Me.ButtonCargarXml)
        Me.Controls.Add(Me.btnLimpiarStatus)
        Me.Controls.Add(Me.txtAreaResultados)
        Me.Controls.Add(Me.cmbPuerto)
        Me.Controls.Add(Me.btnEnviarComando)
        Me.Controls.Add(Me.LabelStatus)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmFirma"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Aplicacion Configuracion Emv"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region

#Region " funciones del testfirma "
    Private Sub CargarParametrosPuerto()
        Dim inParidad As Integer

        Try
            'Se obtiene los datos de configuracion del puerto
            inParidad = 0
            m_ModemPort = CInt(cmbPuerto.Text) '1
            inBaudRate = CInt(115200) '115200
            inDataBit = CInt(8) '8
            inDataParity = inParidad '2
            inDataStopBit = CInt(1) '1
            inTimeOutRecep = 500
            inBufferSize = 131072 '4096

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "INFORMACIÓN DEL SISTEMA")
        End Try
    End Sub

    Private Sub RecibirACK()
        Try
            ' As long as there is information, read one byte at a time and 
            '   output it.
            While (m_CommPort.Read(1) <> -1)
                'bReadRS232 = True
                ' Write the output to the screen.
                If (Chr(m_CommPort.InputStream(0)) = ACK) Then
                    'WriteMessage(Chr(m_CommPort.InputStream(0)), False)
                    bReadRS232 = True
                End If

            End While
        Catch exc As Exception
            ' An exception is raised when there is no information to read.
            '   Don't do anything here, just let the exception go.

        End Try

    End Sub

    Private Sub Recibir_ACK_NAK_EOT()
        Dim ch As Char
        Try
            While (m_CommPort.Read(1) <> -1)
                ch = Chr(m_CommPort.InputStream(0))
                szMensaje = ch
                bReadRS232 = True

                If (ch = ACK) Or (ch = NAK) Or (ch = EOT) Then
                    Exit Sub
                End If
            End While
        Catch exc As Exception
        End Try
    End Sub

    Private Sub RecibirMensaje_STX_ETX()
        Try
            ' As long as there is information, read one byte at a time and output it.
            While (m_CommPort.Read(1) <> -1)
                szMensaje += Chr(m_CommPort.InputStream(0))

                If (szMensaje.Length > 3) Then
                    If (szMensaje.Chars(szMensaje.Length - 2) = ETX) Or (szMensaje.Chars(szMensaje.Length - 2) = SO) Then
                        bReadRS232 = True
                        Exit Sub
                    End If
                End If
            End While
        Catch exc As Exception
            ' An exception is raised when there is no information to read.
            '   Don't do anything here, just let the exception go.
            'MsgBox(exc.Message, MsgBoxStyle.Exclamation, "INFORMACIÓN DEL SISTEMA")
        End Try
    End Sub

    Private Sub RecibirMensaje()
        Try
            ' As long as there is information, read one byte at a time and output it.
            While (m_CommPort.Read(1) <> -1)
                szMensaje += Chr(m_CommPort.InputStream(0))
                bReadRS232 = True
            End While
        Catch exc As Exception
            ' An exception is raised when there is no information to read.
            '   Don't do anything here, just let the exception go.
            'MsgBox(ex.Message, MsgBoxStyle.Exclamation, "INFORMACIÓN DEL SISTEMA")
        End Try
    End Sub

    Private Sub frmFirma_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        time1 = "300"
        time2 = "300"
        modo_despliegue = "1"
        modo_almacenamiento = "1"
        resolucion = "999"
        formato_imagen = "4"
        almacena_flash = "0"
        nombre_archivo = "firma"

        Coord_X1 = "000"
        Coord_X2 = "320"
        Coord_Y1 = "000"
        Coord_Y2 = "220"
        Limpia_Pantalla = "0"

        btKsnengineID = {255, 255, 152, 118, 84, 51, 51, 224} 'dec
        btEngineID = {0, 2} 'dec

        stEngineKeys = {
                "58,C3,F7,6B,8E,B0,86,90,81,8D,54,78,52,6A,EF,11", "45,5B,E6,F4,6A,40,12,74,98,8B,E5,AF,9E,3A,AD,DA",
                "0A,9E,A0,C5,F7,D5,6A,43,92,8B,89,24,8D,A6,20,08", "2F,74,34,D5,9C,C7,C1,BA,CF,B2,66,7F,EA,71,62,33",
                "1C,38,46,02,E7,25,69,02,C0,6C,0B,B6,61,A6,F5,72", "0D,AF,68,15,6C,FA,00,27,D9,64,85,1B,0A,D9,FF,97",
                "EB,B9,9A,01,09,91,0F,45,0C,23,E0,8D,89,E2,24,0A", "F8,24,36,48,D6,78,F3,B0,7C,7D,E2,40,9D,F2,67,60",
                "F0,A1,8F,78,D9,04,EF,5B,2F,64,8F,0A,67,5F,DC,71", "E0,BE,33,18,9B,35,C7,48,32,DE,C4,CD,BA,B8,46,A4",
                "EA,C3,64,75,77,49,04,10,42,3A,91,D2,DE,58,7F,85", "7A,AB,ED,35,69,FB,71,5F,D1,28,9A,23,66,59,7D,E4",
                "0C,F1,3F,5D,B1,1F,2A,E8,CC,92,48,4A,3E,CE,0D,51", "0C,D7,FA,2A,2D,5F,AC,B8,99,A7,29,11,08,58,3A,E6",
                "C1,97,E1,A6,94,16,28,2B,38,0F,9C,A0,1C,F4,F6,4F", "E7,C0,04,58,99,EB,B4,1F,1A,13,6D,FA,B3,03,86,5B",
                "91,C8,4D,B1,05,2E,7E,36,B3,C6,E9,AC,06,39,E5,A2", "8F,96,01,5C,B4,98,24,C8,EE,81,BE,E7,86,4F,FC,F4",
                "98,D3,C9,AC,A6,DE,C1,64,68,CA,7A,70,FF,06,16,62", "39,D0,C1,86,CB,E6,59,4F,77,70,12,32,12,5D,7A,8C",
                "C7,5F,2C,E1,64,37,05,15,39,EA,8E,54,FC,79,20,0D", "78,8F,25,FF,81,CB,A4,4A,5D,87,5F,AB,17,A3,96,75",
                "5A,4C,7C,27,4A,3F,D4,EA,27,5E,4E,5F,49,77,7E,CE", "9B,C7,C8,FB,B2,49,37,C8,95,10,9D,03,AA,27,67,C2",
                "94,81,69,EC,03,80,4B,5B,83,59,85,C2,60,64,C7,2F", "9F,FA,7D,39,DB,FF,AE,0A,14,FC,9F,54,62,43,28,C1",
                "FC,37,C7,99,0A,65,C8,B9,20,C4,99,70,4D,48,18,34", "40,72,F1,DF,0F,BF,22,79,76,FF,FF,85,44,91,3C,54",
                "5D,9A,FA,1B,42,B7,76,22,7C,44,0C,F6,F9,A7,83,D9", "B6,86,87,56,21,1C,6D,48,E1,B6,3E,D2,0E,E7,AB,7B",
                "4A,BC,A4,21,23,16,5B,24,4B,57,70,71,52,90,C4,52", "78,F1,FF,85,05,93,1B,E8,9F,9D,7F,D0,8E,77,18,9D",
                "CF,7C,27,4B,4E,80,26,DF,FE,1D,FC,E6,5C,10,32,CF", "A4,75,8D,64,62,C5,4F,55,98,20,D7,26,92,7B,A5,A3",
                "88,30,9A,A1,47,D9,70,03,36,B3,7D,AA,B0,6B,5E,CB", "8A,52,A6,FE,25,FA,53,9B,74,FD,9B,45,0C,1A,C3,4C",
                "61,5E,FD,FC,D2,B1,56,0D,8E,9A,2E,62,60,1B,B1,66", "8C,0F,5E,0B,4F,15,67,91,65,4E,47,8D,7C,54,84,D1",
                "CB,FC,ED,E1,B7,8F,E9,2E,C5,3E,19,61,BA,5D,73,F7", "82,5B,E3,ED,15,D0,B7,77,83,B0,D6,59,CE,53,70,B7",
                "79,90,B5,A3,AA,46,92,5D,D8,2D,1C,51,7F,26,82,62", "F9,23,E6,C6,DB,A3,C0,81,EE,98,2A,C8,32,A1,13,E8",
                "29,E1,D6,14,AD,BE,D7,6C,4F,FE,5C,40,79,AA,F7,2D", "A8,AA,EB,F0,EF,55,A8,EC,6C,A8,50,71,F5,73,71,09",
                "9A,3B,0C,4E,5F,DA,60,52,56,DC,2F,6F,5D,58,A1,BC", "D4,A0,0F,81,C5,CB,9A,8A,7E,39,67,E8,6B,51,6C,6A",
                "3F,18,B8,3F,3D,1E,29,B2,E9,3B,61,F1,2E,E1,DC,EF", "C9,95,18,65,83,7E,E0,A8,7D,C7,E8,CB,21,9B,60,DE",
                "92,48,94,1E,83,C1,42,20,90,CB,BD,56,7C,62,9A,A8", "D6,7A,AD,73,FB,90,C4,46,2D,06,CF,37,15,A6,84,8B",
                "0C,13,28,3D,48,87,44,AC,A8,57,34,62,E4,A8,BC,FF", "C9,5B,F4,BA,BD,AD,B6,91,C5,AA,0B,78,2C,58,41,6D",
                "F7,3E,3F,0F,7D,5B,C8,16,0C,1D,C7,91,4F,4F,98,F7", "BF,CA,EF,3B,4F,19,FC,71,FD,6D,CC,A5,E3,F7,C7,9F",
                "65,E6,06,18,EB,D0,1C,EA,55,E1,34,B7,68,33,CB,17", "27,18,99,BD,8E,0C,60,E2,4F,EC,F8,30,81,8E,63,C7",
                "AB,3D,C7,4B,8A,AD,9B,BA,9B,3D,B4,69,9A,A8,97,C8", "14,22,99,22,32,16,DA,3E,A3,48,49,5D,D6,43,DD,28",
                "3A,8E,E1,73,1E,A6,88,BB,A3,82,7A,BF,8E,E3,B9,C6", "90,26,AB,F9,21,4A,B8,D7,BE,7F,84,15,18,96,BA,91",
                "14,5C,6B,F4,54,C1,03,8C,48,23,F5,10,F5,E1,F1,48", "90,09,49,23,FF,3B,30,F2,6C,47,9D,F0,BC,58,8F,E1",
                "C3,4D,6E,02,C9,76,A4,0D,FA,A5,4E,B8,68,07,61,79", "EB,C4,73,46,71,6D,D2,47,ED,F2,FB,8D,F6,03,F0,A2",
                "15,0A,97,EE,20,D1,11,42,5D,BC,1C,67,9D,D1,D2,61", "0A,90,59,0B,C1,27,14,58,73,0B,F7,AF,20,3B,3B,1C",
                "69,FF,A4,69,BA,5C,23,AB,7E,73,3D,65,55,D0,D9,1C", "23,9D,66,48,48,52,6A,15,5D,B0,DF,B3,B4,F3,C4,F5",
                "09,7C,77,78,CC,28,FE,D2,71,6A,3E,AC,6A,F2,5D,C5", "C7,DC,F4,62,C1,6D,84,4E,60,4C,91,15,86,85,36,29",
                "3B,43,64,A2,60,7A,68,95,21,DB,09,8C,5C,F7,3A,9F", "D8,AF,64,D6,97,57,89,0B,C9,EE,DF,2E,6D,7B,0C,91",
                "16,D4,40,D0,76,FE,76,11,EB,83,42,D3,B4,DC,04,75", "17,D5,2F,2D,0F,EC,26,09,8E,3D,3F,51,70,18,D1,09",
                "9D,9C,A2,48,95,23,49,15,62,D7,9D,BD,F9,0A,5A,DB", "2C,4C,C6,E0,D6,32,7C,54,F9,E6,EF,1C,45,29,A6,10",
                "8A,36,0E,96,4E,82,CB,75,AD,4B,FC,8F,E6,77,A3,44", "B6,BA,52,00,98,F8,42,A9,C5,4F,76,85,4D,26,AF,4E",
                "76,AF,72,E0,84,A3,1E,1C,99,60,9C,F8,E5,47,D9,38", "A8,54,C6,67,D6,37,B1,99,26,33,9A,18,08,F6,4A,04",
                "21,DA,4B,2C,CA,35,C5,AA,63,01,7D,94,56,F9,75,CB", "B3,6D,21,9D,55,9B,32,05,C5,3F,BB,17,64,D2,9B,FF",
                "3F,3A,19,F5,C8,B5,46,BD,EA,B5,B0,8C,1F,DB,C8,2F", "22,EE,73,6E,D9,C6,CF,20,CB,2D,E9,EE,78,4B,F7,D5",
                "28,73,43,25,3D,07,B7,60,0A,80,44,90,8E,D3,CA,38", "EF,95,6E,2A,A7,EA,64,57,76,D2,21,D5,FE,9D,76,77",
                "82,E6,44,9F,2C,10,DD,FB,F2,64,E6,15,0F,C1,2C,96", "44,17,B0,08,1D,23,60,F1,DD,96,71,E3,61,EE,26,46",
                "75,F7,38,91,84,27,F7,C2,34,AC,DA,72,16,AC,5B,4A", "9C,A4,F9,2B,23,CD,A4,D0,5A,F5,A9,B6,3D,A6,28,AF",
                "E2,68,D8,A3,53,D8,0E,23,C0,5D,43,77,2F,BF,3C,08", "87,26,76,82,91,A2,34,1A,70,04,DB,93,58,5C,18,07",
                "E9,FD,AF,27,77,03,BA,BF,EB,3B,7D,0B,2B,05,06,A7", "00,05,AC,3E,20,46,22,75,6C,B1,E3,84,A4,0E,C7,8D",
                "8F,C2,FA,40,0A,0F,6D,C4,FC,DF,CE,B9,E8,5B,5A,A7", "32,D3,B2,A6,32,89,4A,BA,4D,10,38,29,AB,49,09,07",
                "A4,97,9F,AF,27,59,61,CE,91,D1,1E,25,4E,6B,C9,C2", "63,95,CA,5C,E8,B0,C7,69,76,9D,AA,BC,2C,BF,EA,84",
                "B0,EB,1C,3A,0A,7C,D3,A7,E9,66,A5,26,E0,C6,21,72", "EF,CD,11,E4,48,F9,DA,71,C5,CF,D6,A3,51,8C,3B,57",
                "4D,E6,B9,09,5C,5E,BD,88,5F,B1,7E,D7,5C,25,DE,A8", "13,A7,46,37,9A,B4,AA,3C,12,1E,38,C2,7D,DB,62,1D",
                "57,F4,8F,E5,2F,EA,F7,2F,10,E0,D9,EB,9A,00,46,43", "A8,CC,1B,CD,F3,55,ED,39,A7,6E,80,87,17,A5,09,1C",
                "75,49,EC,98,20,4D,7E,6B,BA,C4,58,DF,3F,FA,AF,7C", "04,1B,89,42,6D,B9,0C,0E,0D,63,CD,55,E1,FB,DA,40",
                "DF,6E,B0,EE,CC,18,40,90,1D,72,9E,60,1B,C5,B9,EE", "44,1B,D3,2F,2E,EA,50,7E,4E,BC,51,56,25,84,2B,85",
                "4B,4E,41,C2,F7,30,A2,91,32,3E,60,AF,1D,4A,53,1E", "CC,55,29,5D,00,CC,36,B8,50,9A,5D,E8,AB,88,DE,34",
                "51,32,65,B4,D4,67,0C,C2,EC,97,AB,E7,0D,F1,15,58", "53,87,7C,34,70,0A,C2,F7,5C,79,B2,22,4A,71,28,8F",
                "2F,CB,BB,96,2D,2F,54,D5,77,01,D0,16,51,22,38,B2", "E1,34,02,12,B6,07,E2,3E,7E,10,C4,82,4F,4C,9F,AC",
                "25,AD,5A,1F,9D,BE,C9,60,F2,6E,AA,01,D6,DF,AD,9B", "E7,97,17,83,EF,BD,07,88,B3,41,E2,BE,31,97,02,0F",
                "DA,94,CC,B1,DD,1A,A5,AC,4D,17,7B,7E,09,C0,37,31", "BD,85,AF,96,89,98,50,9F,5B,27,77,70,43,E1,6F,CB",
                "E3,91,B0,20,F0,3C,F4,31,A5,40,62,2C,DF,9B,8F,F8", "68,C5,45,F3,0E,78,41,B7,D3,5C,DA,D4,43,36,A6,F4",
                "50,96,51,5D,01,5E,A6,0A,29,64,05,45,4E,D0,E4,DE", "3F,26,02,7F,1E,E4,5F,59,52,59,5F,AB,54,C2,F9,67",
                "3F,AE,E9,41,5E,9D,16,24,C5,93,E7,E7,0C,7F,D5,47", "11,F7,AA,90,DF,F6,AE,7E,B2,7E,B9,AE,A7,C2,D8,19",
                "28,85,EE,DC,A7,2C,B8,17,19,DF,5A,83,70,F1,48,25", "94,34,0F,9A,11,C7,C9,7A,D8,7E,6E,A4,6A,9E,E0,22",
                "1B,A8,96,A3,88,25,D9,E5,5E,08,39,F6,A7,4D,BE,F5", "01,F1,A3,AA,E9,E0,05,60,9D,20,C9,BF,06,62,23,C7",
                "A3,F6,96,B4,C9,65,52,15,DE,C6,B7,A7,CC,95,B3,10", "EC,B0,68,F3,A7,1D,51,0E,9E,56,52,21,AC,1B,41,7B",
                "6D,16,70,B8,1C,77,E0,AD,14,D2,72,5F,0E,E8,4A,BA", "18,31,C8,4E,A5,4F,13,21,4F,F8,82,39,3F,6C,7F,E6",
                "FA,E3,90,6E,16,68,5C,CF,92,20,0B,84,2B,BD,7B,F3", "FE,3F,75,4C,8B,74,3D,09,14,9C,4A,B4,24,47,61,8F",
                "E2,4E,9A,B3,C3,D3,EF,45,8A,FB,C8,52,1E,23,D5,52", "9B,9E,83,50,85,1A,57,FA,1A,BC,E9,1E,8D,1A,70,DB",
                "41,2B,D5,6C,61,8D,C3,F4,9B,28,A5,BC,EA,8D,79,F0", "03,9B,69,35,C2,52,F3,98,DD,E4,74,3B,06,52,6C,84",
                "C3,11,62,08,A7,86,34,20,88,2E,06,A0,10,5C,D0,4F", "28,3E,1F,FC,00,CC,CF,97,33,4A,CE,06,3F,22,4F,95",
                "4F,EA,FE,81,B1,6F,6D,7B,CB,E6,1E,EC,B1,34,33,1E", "F8,7F,28,D9,03,93,84,43,12,62,43,DD,82,E6,74,A5",
                "E5,FA,34,3B,5E,4D,78,6A,A3,A5,C4,A0,4A,8F,0C,76", "CA,AB,66,E9,F6,B4,51,77,EC,DC,FF,00,4E,44,28,80",
                "40,76,A3,D6,4F,01,F4,22,4C,5D,2F,45,6C,26,66,C9", "4B,24,E7,4D,2E,D6,20,CF,39,C1,DF,4F,0D,B4,AE,5A",
                "90,0D,8D,FA,63,38,5C,72,53,6C,72,70,C1,A0,D9,13", "8D,9D,E9,C9,9D,58,08,A0,89,C7,50,84,7D,05,18,7A",
                "CC,DA,BB,A2,0A,7C,36,C4,F6,40,9C,7A,77,75,12,B2", "45,48,B6,19,93,8A,1C,EA,5B,61,D4,59,9F,12,C0,0E",
                "D9,4E,B0,D5,53,70,5E,A8,27,B3,8F,C4,D5,DA,09,34", "47,BF,3A,74,AD,E1,15,38,C4,09,43,8B,43,36,BA,AB",
                "FC,D8,26,40,EA,69,E0,F5,53,89,40,0A,3A,B3,0D,49", "A6,40,66,3E,EB,FC,50,61,5E,4C,A5,7F,B9,CD,3B,FF",
                "1F,7D,2E,B6,B0,1E,38,D4,1D,25,80,5E,C7,03,6F,F0", "21,45,47,C6,9A,21,27,EC,FB,FF,7E,84,E2,5A,35,AF",
                "A5,45,B0,BD,5E,EC,D9,A1,3B,B6,BA,88,C2,1B,66,78", "FB,90,F6,68,6A,57,ED,92,2A,B2,20,39,3B,59,F7,91",
                "49,21,47,F1,12,BE,5E,B6,23,EA,21,78,6F,20,93,FC", "71,48,05,6B,EA,FB,A0,BD,DC,1A,08,7D,72,35,3F,EE",
                "C1,5B,39,EC,FD,84,FA,D8,BC,3A,4F,5F,05,3D,B8,AA", "66,84,C4,3C,97,35,F7,35,94,1C,AE,2C,95,86,5B,64",
                "61,BC,AE,B7,2B,29,35,6D,D0,CD,97,CF,9A,EC,23,20", "A6,81,31,4F,4F,0C,43,1A,A6,B7,43,EB,2C,B5,B8,A2",
                "36,59,BB,6F,83,45,7A,63,13,2C,BF,24,25,96,5D,A6", "18,F8,47,0C,A5,41,F8,2F,EB,1D,A4,70,43,BF,21,E7",
                "9B,0A,7C,D6,36,F8,8B,8A,2D,06,AE,E0,0B,16,D2,68", "58,E8,67,16,5C,54,F2,19,BF,7C,66,F6,91,99,2B,CE",
                "7E,AB,5C,C4,0C,E4,F3,67,90,AD,B0,1F,49,C9,C4,F2", "CA,5D,E2,B1,29,50,01,2B,98,B0,A1,67,7A,C4,04,7D",
                "9B,9B,5C,36,FE,BD,0E,12,19,E8,8B,71,95,4A,E0,0B", "61,9D,9B,89,26,94,5C,0B,F2,40,01,DA,1B,AE,4F,61",
                "9B,A0,BF,97,50,97,88,51,CF,BA,2D,7B,29,BB,73,63", "87,B3,DB,0D,2E,6D,62,0A,EA,A1,66,DB,0A,95,E4,3C",
                "A1,70,89,33,81,A1,69,AC,1D,B2,A4,3F,38,2D,57,B9", "F3,06,4A,36,55,0D,3F,70,7E,4D,69,41,96,05,88,A9",
                "B5,23,42,F1,DA,86,07,FB,D1,75,D0,BA,C5,56,7D,9A", "6E,75,F0,53,7D,82,D5,A8,CB,34,DC,19,33,2A,DF,E1",
                "34,FA,B0,35,55,87,0D,80,64,F4,97,3D,E3,60,B2,D5", "0F,65,CD,27,72,2B,1B,61,73,EC,76,0E,F0,37,22,83",
                "92,17,51,96,6D,DF,94,94,C1,7E,15,88,92,79,23,73", "FF,00,F0,B1,83,F0,F3,E6,91,11,BC,99,1B,0C,CE,2B",
                "00,D9,20,D6,5B,60,10,5E,26,8A,43,F6,F7,72,EA,79", "A7,C7,9A,B5,44,E4,1C,78,D4,BE,06,4B,0B,17,7C,C2",
                "65,7E,11,05,AD,CE,E2,C1,20,54,5B,4A,99,40,52,B2", "DC,EE,CA,91,74,18,34,BF,E5,30,C6,A9,74,A5,1D,F3",
                "37,D7,95,37,D7,F0,38,A4,B9,86,39,47,5D,F3,91,92", "BC,49,17,2F,67,F0,1A,02,0C,6C,34,0C,5E,BD,88,3B",
                "FF,6F,DF,47,65,96,FF,8B,08,00,52,9C,2D,37,30,79", "F7,50,A2,65,40,36,B5,7E,23,9D,57,92,C9,8E,51,C6",
                "37,0A,03,4B,FD,75,33,AB,0F,7F,C4,83,27,EE,C1,A9", "5C,3A,05,61,83,D7,E1,12,D7,6E,36,2B,82,E1,E4,D6",
                "32,EF,F4,E4,D5,8D,75,0E,C0,5A,5B,94,BC,BF,76,BC", "CE,B6,9E,3B,07,55,9C,C7,67,C6,69,68,06,7F,B0,DC",
                "51,77,62,A9,30,27,51,66,4D,5A,E1,64,13,52,71,3D", "A8,63,B1,12,8E,B8,8C,6B,46,71,98,13,6E,5C,7F,1B",
                "83,28,19,73,15,88,59,47,65,D4,B1,7A,8F,87,EC,21", "53,05,A6,63,2F,1E,B6,54,1D,C6,3B,B5,E5,F4,42,A4",
                "BA,3F,A3,3B,5E,71,D9,CD,5B,5B,2F,77,B7,6F,68,17", "62,21,A2,99,D5,54,7D,FD,BB,3B,2D,20,08,3E,46,F2"}

        'Selecciona los combos
        cmbPuerto.SelectedIndex = 8
    End Sub

    Private Function ArmarPaquete(ByRef szPaquete As String, ByRef text As String) As Boolean
        Dim bRetorno As Boolean
        Dim i As Integer = 0
        Dim j As Integer

        szPaquete = ""
        bRetorno = False
        szMensaje = text

        If szMensaje.Length = 0 Then
            Return bRetorno
        End If

        Do
            j = szMensaje.IndexOfAny(",", i)
            If (j > -1) Then
                bRetorno = True
                szPaquete = szPaquete + Chr(CInt(szMensaje.Substring(i, j)))
                If (szMensaje.Length >= j + 1) Then
                    szMensaje = szMensaje.Substring(j + 1)
                End If
            Else
                szPaquete = szPaquete + Chr(CInt(szMensaje))
                bRetorno = True
            End If
        Loop While (j > -1)


        Return bRetorno
    End Function

    Private Sub EscribirMensaje(ByVal szMensaje As String, ByVal szMes1 As String, ByVal inType As Integer)
        Dim i As Integer
        Dim j As Integer
        Dim inValor As Integer
        Dim szTexto As String
        Dim szCaracter As String

        i = 0
        j = (szMensaje.Length - 1)
        szTexto = ""

        If (j >= 0) Then
            Do
                inValor = Asc(szMensaje.Substring(i, 1))

                Select Case inValor
                    Case 0
                        If (inType = 0) Then
                            szCaracter = "<NULL>"
                        Else
                            szCaracter = szMensaje.Substring(i, 1)
                        End If

                    Case 2
                        If (inType = 0) Then
                            szCaracter = "<STX>"
                        Else
                            szCaracter = szMensaje.Substring(i, 1)
                        End If

                    Case 3
                        If (inType = 0) Then
                            szCaracter = "<ETX>"
                        Else
                            szCaracter = szMensaje.Substring(i, 1)
                        End If

                    Case 4
                        If (inType = 0) Then
                            szCaracter = "<EOT>"
                        Else
                            szCaracter = szMensaje.Substring(i, 1)
                        End If

                    Case 6
                        If (inType = 0) Then
                            szCaracter = "<ACK>"
                        Else
                            szCaracter = szMensaje.Substring(i, 1)
                        End If

                    Case 14
                        If (inType = 0) Then
                            szCaracter = "<SO>"
                        Else
                            szCaracter = szMensaje.Substring(i, 1)
                        End If

                    Case 15
                        If (inType = 0) Then
                            szCaracter = "<SI>"
                        Else
                            szCaracter = szMensaje.Substring(i, 1)
                        End If

                    Case 21
                        If (inType = 0) Then
                            szCaracter = "<NAK>"
                        Else
                            szCaracter = szMensaje.Substring(i, 1)
                        End If

                    Case 28
                        If (inType = 0) Then
                            szCaracter = "<FS>"
                        Else
                            szCaracter = szMensaje.Substring(i, 1)
                        End If

                    Case Else
                        szCaracter = szMensaje.Substring(i, 1)
                End Select

                If (inType = 0) Then
                    'Mostrar el mensaje en ASC
                    If (i <> j) Then
                        szTexto = szTexto + szCaracter
                    Else
                        If (szCaracter.Length = 1) Then
                            szTexto = szTexto + CStr(Asc(szCaracter)) 'LRC
                        Else
                            szTexto = szTexto + szCaracter
                        End If
                    End If
                Else
                    'Mostrar el Mensaje en Hexa
                    If (i <> j) Then
                        If (i = 0) Then
                            szTexto = szTexto + Pad(CStr(Hex(Asc(szCaracter))), "0", 2, PAD_RIGHT)
                        Else
                            szTexto = szTexto + ", " + Pad(CStr(Hex(Asc(szCaracter))), "0", 2, PAD_RIGHT)
                        End If
                    Else
                        If (j = 0) Then
                            If (i = 0) Then
                                szTexto = szTexto + Pad(CStr(Hex(Asc(szCaracter))), "0", 2, PAD_RIGHT)
                            Else
                                szTexto = szTexto + ", " + Pad(CStr(Hex(Asc(szCaracter))), "0", 2, PAD_RIGHT)
                            End If
                        Else
                            szTexto = szTexto + ", " + Pad(CStr(Hex(Asc(szCaracter))), "0", 2, PAD_RIGHT)

                        End If
                    End If
                End If

                i = i + 1
            Loop Until (i > j)
        End If

        LblStatusFocus()
        SetLblStatusReadOnly(False)

        If ((Me.txtAreaResultados.Text.Length + Text.Length) >= 552152) Then
            SetLblStatusText(String.Empty)
        End If

        SetLblStatusSelectedText(szMes1 + szTexto + Microsoft.VisualBasic.vbCrLf)
        LblStatusScrollToCaret()
        SetLblStatusReadOnly(True)
        LblStatusRefresh()
    End Sub

    Private Sub LblStatusFocus()
        If Me.txtAreaResultados.InvokeRequired = True Then
            Dim d As New LblStatusFocusCallback(AddressOf LblStatusFocus)
            Me.Invoke(d)
        Else
            Me.txtAreaResultados.Focus()
        End If
    End Sub

    Private Sub SetLblStatusReadOnly(ByVal readOnlyFlag As Boolean)
        If Me.txtAreaResultados.InvokeRequired = True Then
            Dim d As New SetLblStatusReadOnlyCallback(AddressOf SetLblStatusReadOnly)
            Me.Invoke(d, New Object() {readOnlyFlag})
        Else
            Me.txtAreaResultados.ReadOnly = readOnlyFlag
        End If
    End Sub

    Private Sub SetLblStatusText(ByVal text As String)
        If Me.txtAreaResultados.InvokeRequired = True Then
            Dim d As New SetLblStatusTextCallback(AddressOf SetLblStatusText)
            Me.Invoke(d, New Object() {text})
        Else
            Me.txtAreaResultados.Text = text
        End If
    End Sub

    Private Sub SetLblStatusSelectedText(ByVal text As String)
        If Me.txtAreaResultados.InvokeRequired = True Then
            Dim d As New SetLblStatusSelectedTextCallback(AddressOf SetLblStatusSelectedText)
            Me.Invoke(d, New Object() {text})
        Else
            Me.txtAreaResultados.SelectedText = text
        End If
    End Sub

    Private Sub LblStatusScrollToCaret()
        If Me.txtAreaResultados.InvokeRequired = True Then
            Dim d As New LblStatusScrollToCaretCallback(AddressOf LblStatusScrollToCaret)
            Me.Invoke(d)
        Else
            Me.txtAreaResultados.ScrollToCaret()
        End If
    End Sub

    Private Sub LblStatusRefresh()
        If Me.txtAreaResultados.InvokeRequired = True Then
            Dim d As New LblStatusRefreshCallback(AddressOf LblStatusRefresh)
            Me.Invoke(d)
        Else
            Me.txtAreaResultados.Refresh()
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim inIntentos As Integer
        Dim i As Integer

        If (inRecibioACK = 1) Then
            ' Valida si se recibe respuesta
            inIntentos = 0

            Do
                'Parametros de inicialización de Timer
                'bExpiroTimer = False
                FechaInactivo = Now
                szMensaje = ""

                While (Check_TimeOut(inDuracionTimerSegundos) = False) 'And (szMensaje.Length = 0)
                    RecibirMensaje()
                End While

                inIntentos = inIntentos + 1

                If (szMensaje.Length > 0) Then
                    EscribirMensaje(szMensaje, "Recibió (ASC): ", 0)
                    EscribirMensaje(szMensaje, "Recibió (HEX): ", 1)

                End If
            Loop Until (inIntentos = 3)
        End If
    End Sub

    Private Function Check_TimeOut(ByVal inTimeoutSecs As Integer) As Boolean
        Dim lnSegundos As Long

        lnSegundos = DateDiff(DateInterval.Second, Now, FechaInactivo)

        If lnSegundos < 0 Then
            lnSegundos = lnSegundos * (-1)

            If inTimeoutSecs > 0 Then
                If lnSegundos >= inTimeoutSecs Then
                    Return True
                End If
            End If
        End If

        Return False
    End Function

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpiarStatus.Click
        Me.txtAreaResultados.Text = ""
    End Sub

    Private Sub DisablePortConfigControls()
        Me.cmbPuerto.Enabled = False
    End Sub

    Private Sub DisableControlsWhileWaiting()
        Me.btnLimpiarStatus.Enabled = False
        Me.btnEnviarComando.Enabled = False

    End Sub

    Private Sub EnableControlsAfterWaiting()
        Me.btnLimpiarStatus.Enabled = True
        Me.btnEnviarComando.Enabled = True

    End Sub

#End Region

#Region " funciones nuevas"

    '------------------------------------------------------------------------------'
    Private Function Enviar_Mensaje(ByVal szMensaje As Byte()) As Boolean
        Try
            'Enviando el mensaje a traves del puerto serial
            m_CommPort.Write(szMensaje)
            SetLblStatusReadOnly(False)
            SetLblStatusSelectedText(vbCrLf)
            SetLblStatusReadOnly(True)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "INFORMACIÓN DEL SISTEMA")
            Return False
        End Try
    End Function

    Private Function CalcularCRC1(ByVal text As String) As Integer
        Dim szMensaje As String = text
        Dim i, y, z As Integer
        Dim Pos1 As Integer = 0
        Dim value As Integer
        Dim octet As String
        Dim topbit As Integer = 0
        Dim register As Integer = 0
        Dim MSB_Mask As Integer = 32768
        Dim Mask As Integer = 65535
        Dim Poly As Integer = 4129
        Dim Widht As Integer = 15
        Dim ArrCadena As String() = szMensaje.Split(",")
        Dim ReflectOut As Integer = 0

        Dim ciclo1 As Integer = (ArrCadena.Length) - 1


        For i = 0 To ciclo1

            octet = ArrCadena(i)
            value = Convert.ToInt32(octet)

            For y = 0 To 7
                topbit = register And MSB_Mask
                register = ((register << 1) And Mask) Or ((value >> (7 - y)) And &H1)

                If topbit <> 0 Then
                    register = register Xor Poly
                End If
            Next
        Next


        For z = 0 To Widht
            topbit = register And MSB_Mask
            register = ((register << 1) And Mask)
            If topbit <> 0 Then
                register = register Xor Poly
            End If
        Next

        'EscribirMensaje("", "1)Recibió: (" + Convert.ToString(register) + ")", 0)
        'EscribirMensaje("", "2)CRC1: (" + Convert.ToString(register >> 8) + ")", 0)

        Return register >> 8
    End Function

    Private Function CalcularCRC2(ByVal text As String) As Integer
        Dim szMensaje As String = text
        Dim i, y, z As Integer
        Dim Pos1 As Integer = 0
        Dim value As Integer
        Dim octet As String
        Dim topbit As Integer = 0
        Dim register As UInt16 = 0
        Dim MSB_Mask As Integer = 32768
        Dim Mask As Integer = 65535
        Dim Poly As Integer = 4129
        Dim Widht As Integer = 15

        Dim ArrCadena As String() = szMensaje.Split(",")

        Dim ciclo1 As Integer = (ArrCadena.Length) - 1


        For i = 0 To ciclo1

            octet = ArrCadena(i)
            value = Convert.ToInt32(octet)
            Pos1 = Pos1 + 2

            For y = 0 To 7
                topbit = register And MSB_Mask
                register = ((register << 1) And Mask) Or ((value >> (7 - y)) And &H1)

                If topbit <> 0 Then
                    register = register Xor Poly
                End If
            Next
        Next


        For z = 0 To Widht
            topbit = register And MSB_Mask
            register = ((register << 1) And Mask)
            If topbit <> 0 Then
                register = register Xor Poly
            End If
        Next

        register = register << 8
        register = register >> 8

        'EscribirMensaje("", "3)CRC2: (" + Convert.ToString(register) + ")", 0)

        Return register
    End Function

    Private Function AbrirPuerto() As Integer
        'Abriendo el  Puerto Serial
        Try
            If (m_CommPort.IsOpen = True) Then
                EscribirMensaje("", "*", 0)
                EscribirMensaje("", "El Puerto COM" + cmbPuerto.Text + "está abierto", 0)
                EscribirMensaje("", "*", 0)
            Else
                m_CommPort.Open(m_ModemPort, inBaudRate, inDataBit, inDataParity, inDataStopBit, inBufferSize)
                EscribirMensaje("", "*", 0)
                EscribirMensaje("", "*----------Se Abrió el Puerto COM" + cmbPuerto.Text + " ------------------------*", 0)
                EscribirMensaje("", "*", 0)
            End If

            DisablePortConfigControls()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "INFORMACIÓN DEL SISTEMA")
        End Try
        Return Nothing
    End Function

    Private Function CerrarPuerto() As Integer

        'Cerrando el Puerto Serial
        m_CommPort.Close()
        EscribirMensaje("", "*----------Se Cerró el Puerto COM" + cmbPuerto.Text + " ------------------------*", 0)
        cmbPuerto.Enabled = True

        Return Nothing
    End Function

    '------------------------------------------------------------------------------'
    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnviarComando.Click
        btnEnviarComando.Enabled = False

        Dim szPaquete As String = String.Empty
        Dim inIntentos As Integer
        Dim i As Integer = 0
        Dim CRC1, CRC2 As Integer
        Dim data2 As [Byte]()
        Dim data1 As [Byte]()
        Dim btMensaje() As [Byte]
        Dim bLogico As Boolean
        Dim rnd As New Random()
        Dim MsjConc As String = ""
        blTipoProceso = False
        '----------------------------------------------------------------------------------------
        inPosComando = 0
        bBanderaComando = True
        inContadorComandos = 3
        '----------------------------------------------------------------------------------------'
        CargarParametrosPuerto()
        AbrirPuerto()
        '----------------------------------------------------------------------------------------'
        rnd.NextBytes(btServerRandom)
        rnd.NextBytes(btServerToken)

        For pos As Integer = 0 To btServerRandom.GetUpperBound(0)
            MsjConc = MsjConc + String.Format("{0}{1}", ",", Convert.ToInt32(btServerRandom(pos)))
        Next
        'atuenticacion primer paso
        stComandos(0) = "02,65,00,20,32,17,00,01" + MsjConc + ",03"

        EscribirMensaje("", "*----------Iniciando Autenticacion Mutua Paso 1------------*", 0)
        EscribirMensaje("", "*", 0)
        '----------------------------------------------------------------------------------------'

        Do
            bLogico = ArmarPaquete(szPaquete, stComandos(inPosComando))
            'Console.Write("!" & vbCrLf)

            If szPaquete.Length = 0 Then
                Exit Sub
            End If

            CRC1 = CalcularCRC1(stComandos(inPosComando))
            CRC2 = CalcularCRC2(stComandos(inPosComando))

            If (bLogico = True) Then

                data1 = Encoding.Default.GetBytes(szPaquete)
                data2 = {CRC1, CRC2}
                ReDim btMensaje(data1.Length + data2.Length)
                data1.CopyTo(btMensaje, 0)
                data2.CopyTo(btMensaje, data1.Length)
                'EscribirMensaje(stComandos(inPosComando), vbCrLf & " MSJ ENVIADO: ", 0)

                'Enviando el mensaje a traves del puerto serial
                If (Enviar_Mensaje(btMensaje) = False) Then
                    Exit Sub
                End If

                FechaInactivo = Now
                szMensaje = ""

                '15 segundos es el tiempo estándar que se debe esperar por un ACK/NAK/EOT
                While (Check_TimeOut(15) = False) And (szMensaje.Length = 0)
                    Recibir_ACK_NAK_EOT()
                End While


                If (szMensaje.Length > 0) Then
                    'EscribirMensaje(szMensaje, vbCrLf & "1) Recibió (HEX): ", 1)
                End If

                If (szMensaje.Length > 0) And (szMensaje = ACK) Then
                    ' Valida si se recibe respuesta
                    inIntentos = 0

                    Do
                        FechaInactivo = Now
                        szMensaje = ""
                        bReadRS232 = False

                        Do
                            RecibirMensaje_STX_ETX()
                        Loop While (Check_TimeOut(1) = False) And (bReadRS232 = False)

                        inIntentos = inIntentos + 1

                        '---------------------------------------------------------------------------------
                        If (inPosComando = 0) Then
                            If (szMensaje.Length > 42) Then
                                Autenticacion1Paso(MsjConc)
                                inIntentos = 3
                            ElseIf (inIntentos = 3) Then
                                inPosComando = inContadorComandos + 1
                            End If
                        ElseIf (inPosComando = 1) Then
                            If (szMensaje.Length > 34) Then
                                Autenticacion2Paso()
                                inIntentos = 3
                            ElseIf (inIntentos = 3) Then
                                inPosComando = inContadorComandos + 1
                            End If
                        ElseIf (inPosComando = 2) Then
                            If (szMensaje.Length > 6) Then
                                EMV_ON()
                                inIntentos = 3
                            ElseIf (inIntentos = 3) Then
                                inPosComando = inContadorComandos + 1
                            End If
                        ElseIf (inPosComando = 3) Then
                            If (szMensaje.Length > 6) Then
                                CONF_TERM()
                                inIntentos = 3
                            ElseIf (inIntentos = 3) Then
                                inPosComando = inContadorComandos + 1
                            End If
                        ElseIf (inPosComando <= (3 + inCantRegistros)) Then
                            If (szMensaje.Length > 6) Then
                                CONF_AID_CAPK()
                                inIntentos = 3
                            ElseIf (inIntentos = 3) Then
                                inPosComando = inContadorComandos + 1
                            End If
                        ElseIf (inPosComando = inContadorComandos) Then
                            If (szMensaje.Length > 0) Then

                                'EscribirMensaje(szMensaje, vbCrLf & "2) Respuesta EMV off (HEX): ", 1)

                                Dim resHexa() As Byte = Encoding.Default.GetBytes(szMensaje)

                                Dim SW1 As Integer = resHexa(4)
                                Dim SW2 As Integer = resHexa(5)
                                Enviar_Mensaje(Encoding.ASCII.GetBytes(ACK))

                                If (SW1 = 144 And SW2 = 0) Then '90 00
                                    EscribirMensaje("", "*----------Finalizacion de la  configuracion emv exitosa --*", 0)
                                    EscribirMensaje("", "*", 0)

                                End If
                                inIntentos = 3
                            End If
                        End If
                        '-----------------------------------------------------------------------
                    Loop Until (inIntentos = 3)
                End If
            End If

            If (bBanderaComando = True) Then
                inPosComando = inPosComando + 1
            End If

        Loop While (inPosComando <= inContadorComandos)

        CerrarPuerto()

        btnEnviarComando.Enabled = True

    End Sub

    Private Function authStep1(ByVal bytes1 As Byte()) As Byte()

        Dim CipherData(31) As Byte
        Dim KsnData(9) As Byte

        For pos As Integer = 0 To 31
            CipherData(pos) = bytes1(pos + 6)
        Next

        For pos As Integer = 0 To 9
            KsnData(pos) = bytes1(pos + 38)
        Next


        Dim JackData As Byte() = sredDecryptCiphered32Ksn10(CipherData, KsnData)

        Dim JackToken(15) As Byte
        Dim JackRandom(15) As Byte

        For pos As Integer = 0 To 15
            JackToken(pos) = JackData(pos)
            JackRandom(pos) = JackData(16 + pos)
        Next

        btSessionKey = xorDataWithData(btServerRandom, JackRandom)

        Dim challengeData As Byte()
        ReDim challengeData(31)

        For pos As Integer = 0 To (JackToken.Length - 1)
            challengeData(pos) = JackToken(pos)
        Next

        For pos As Integer = 0 To (btServerToken.Length - 1)
            challengeData(pos + 16) = btServerToken(pos)
        Next

        TripleDES.userKey = btSessionKey
        Dim ECBTripleDES As New TripleDES(CipherMode.CBC)

        Dim challenge As Byte() = ECBTripleDES.EncryptDataToMemory(challengeData)

        Return challenge
    End Function

    Private Function authStep2(ByVal bytes2 As Byte()) As Byte()

        Dim msjEnc(31) As Byte

        For pos As Integer = 0 To 31
            msjEnc(pos) = bytes2(pos + 6)
        Next

        TripleDES.userKey = btSessionKey
        Dim ECBTripleDES As New TripleDES(CipherMode.CBC)

        Dim challenge As Byte() = ECBTripleDES.DecryptDataFromMemory(msjEnc)

        Return challenge
    End Function

    Private Function sredDecryptCiphered32Ksn10(ByVal CipherData As Byte(), ByVal KsnData As Byte()) As Byte()
        Dim Key1 As Byte() = getDukptKey(KsnData)
        Dim Key2 As Byte() = makeSredKey(Key1)

        TripleDES.userKey = Key2
        Dim ECBTripleDES As New TripleDES(CipherMode.CBC)

        Dim d As Byte() = ECBTripleDES.DecryptDataFromMemory(CipherData)

        Return d
    End Function

    Private Function getDukptKey(ByVal ksn As Byte()) As Byte()

        Dim Ksn_engineID(7) As Byte
        Dim keyID(1) As Byte

        For pos As Integer = 0 To 7
            Ksn_engineID(pos) = ksn(pos)
        Next

        For pos As Integer = 0 To 1
            keyID(pos) = ksn(pos + 8)
        Next

        Dim skeyID As Int32 = (Convert.ToInt32(keyID(0)) << 8) + Convert.ToInt32(keyID(1))

        Dim bandera As Boolean = AreArraysEqual(Ksn_engineID, btKsnengineID)

        If (bandera = True) Then

            Dim KeyIndex As Integer
            Dim KsnOffset As Integer

            KeyIndex = btEngineID(0)
            KsnOffset = btEngineID(1)
            KeyIndex = KeyIndex << 8
            KsnOffset = KeyIndex + KsnOffset


            Dim i As Integer = 0
            Dim j As Integer
            Dim szPaquete1 As String = String.Empty
            Dim llaveDukpt As String = stEngineKeys(skeyID - KsnOffset)

            Do
                j = llaveDukpt.IndexOfAny(",", i)
                If (j > -1) Then
                    szPaquete1 = szPaquete1 + Chr(CInt(Convert.ToInt32(llaveDukpt.Substring(i, j), 16)))
                    If (llaveDukpt.Length >= j + 1) Then
                        llaveDukpt = llaveDukpt.Substring(j + 1)
                    End If
                Else
                    szPaquete1 = szPaquete1 + Chr(CInt(Convert.ToInt32(llaveDukpt, 16)))
                End If
            Loop While (j > -1)

            btDukptKey = Encoding.Default.GetBytes(szPaquete1)
        End If


        Return btDukptKey
    End Function

    Private Function makeSredKey(ByVal key1 As Byte()) As Byte()

        Dim sredData As Byte() = {0, 0, 0, 0, 0, 255, 0, 0, 0, 0, 0, 0, 0, 255, 0, 0}
        Dim msgInData As Byte() = xorDataWithData(key1, sredData)
        Dim key(23) As Byte

        msgInData.CopyTo(key, 0)

        For pos As Integer = 16 To 23
            key(pos) = msgInData(pos - 16)
        Next

        TripleDES.userKey = key
        Dim ECBTripleDES As New TripleDES(CipherMode.ECB)

        Dim msgOut As Byte() = ECBTripleDES.EncryptDataToMemory(msgInData)


        Return msgOut
    End Function

    Private Function getCMACForData1(ByVal serial As Byte()) As Byte()
        Dim data() As Byte = {&HF0, &H2B, &H5F, &H36, &H1, &H2, &H5F, &H2A, &H2, &H9, &H37, &H9F, &H33, &H3, &HE0, &HF8, &HC8, &H9F, &H35, &H1, &H22, &H9F, &H40, &H5, &HF0, &H0, &HB0, &HA0, &H1, &H9F, &H1A, &H2, &H8, &H62, &H9F, &H1E, &H8, &H31, &H32, &H33, &H34, &H35, &H36, &H37, &H38}

        Dim posicion, posicion2 As Integer

        posicion2 = 0
        For pos As Integer = 1 To 10
            If (pos <> 3 And pos <> 7) Then
                posicion = (((data.Length - 1) - 7) + (posicion2))
                data(posicion) = serial(pos)
                posicion2 = posicion2 + 1
            End If
        Next

        Dim cmac(7) As Byte

        Dim key As Byte() = btSessionKey

        If (key.Length <> 16) Then
            Return Nothing
        End If
        TripleDES.userKey = key
        Dim ECBTripleDES As New TripleDES(CipherMode.CBC)


        Dim key1(7) As Byte
        Dim key2(7) As Byte
        Dim subkey As Byte()

        generateSubkeys(key, key1, key2)

        Dim inData As Byte() = data
        Dim inDatalen As Integer = inData.Length
        Dim padSize As Integer = 8 - (inDatalen Mod 8)


        If (padSize = 8 And inDatalen <> 0) Then
            subkey = key1
        Else
            Dim padBytes(padSize - 1) As Byte
            padBytes(0) = &H80

            For pos As Integer = 1 To (padBytes.Length - 1)
                padBytes(pos) = &H0
            Next

            ReDim inData(data.Length + padBytes.Length - 1)
            data.CopyTo(inData, 0)
            padBytes.CopyTo(inData, data.Length)
            subkey = key2

        End If

        Dim last8bytes(7) As Byte
        For pos As Integer = 40 To 47
            last8bytes(pos - 40) = inData(pos)
        Next

        Dim replacementData As Byte() = xorDataWithData(last8bytes, subkey)
        For pos As Integer = 40 To 47
            inData(pos) = replacementData(pos - 40)
        Next

        For pos As Integer = 0 To 7
            cmac(pos) = &H0
        Next

        Dim index As Integer = 0

        While (index < inDatalen)
            Dim range(7) As Byte

            For pos As Integer = 0 To 7
                range(pos) = inData(pos + index)
            Next

            cmac = xorDataWithData(cmac, range)

            cmac = ECBTripleDES.EncryptDataToMemory(cmac)

            index = index + 8
        End While


        Return cmac
    End Function

    Private Function getCMACForData2(ByVal comando As Byte()) As Byte()
        Dim data(comando.Length - 9) As Byte
        Dim cant As Integer = data.Length


        For pos As Integer = 0 To (comando.Length - 9)
            data(pos) = comando(pos + 8)
        Next


        Dim cmac(7) As Byte

        Dim key As Byte() = btSessionKey

        If (key.Length <> 16) Then
            Return Nothing
        End If

        TripleDES.userKey = key
        Dim ECBTripleDES As New TripleDES(CipherMode.CBC)


        Dim key1(7) As Byte
        Dim key2(7) As Byte
        Dim subkey As Byte()

        generateSubkeys(key, key1, key2)

        Dim inData As Byte() = data
        Dim inDatalen As Integer = inData.Length
        Dim padSize As Integer = 8 - (inDatalen Mod 8)


        If (padSize = 8 And inDatalen <> 0) Then
            subkey = key1
        Else
            Dim padBytes(padSize - 1) As Byte
            padBytes(0) = &H80

            For pos As Integer = 1 To (padBytes.Length - 1)
                padBytes(pos) = &H0
            Next

            ReDim inData(data.Length + padBytes.Length - 1)
            data.CopyTo(inData, 0)
            padBytes.CopyTo(inData, data.Length)
            subkey = key2

        End If

        Dim last8bytes(7) As Byte

        For pos As Integer = ((inData.Length) - 8) To (inData.Length - 1)
            Dim temp As Integer = (pos - ((inData.Length - 1) - 8))
            last8bytes(pos - ((inData.Length) - 8)) = inData(pos)
        Next

        Dim replacementData As Byte() = xorDataWithData(last8bytes, subkey)

        For pos As Integer = ((inData.Length) - 8) To (inData.Length - 1)
            Dim temp As Integer = (pos - ((inData.Length - 1) - 8))
            inData(pos) = replacementData(pos - ((inData.Length) - 8))
        Next

        For pos As Integer = 0 To 7
            cmac(pos) = &H0
        Next

        Dim index As Integer = 0

        While (index < inDatalen)
            Dim range(7) As Byte

            For pos As Integer = 0 To 7
                range(pos) = inData(pos + index)
            Next

            cmac = xorDataWithData(cmac, range)

            cmac = ECBTripleDES.EncryptDataToMemory(cmac)

            index = index + 8
        End While


        Return cmac
    End Function

    Private Function generateSubkeys(ByVal keyIn As Byte(), ByVal key1Out As Byte(), ByVal key2Out As Byte()) As Integer
        Dim zeroData(7) As Byte

        For pos As Integer = 0 To 7
            zeroData(pos) = 0
        Next

        TripleDES.userKey = keyIn
        Dim ECBTripleDES As New TripleDES(CipherMode.CBC)
        Dim encryptedData As Byte() = ECBTripleDES.EncryptDataToMemory(zeroData)

        Dim bytebuffer As Long
        Dim key0 As Long
        For pos As Integer = 0 To 7
            bytebuffer = encryptedData(pos)
            key0 = key0 + (bytebuffer << ((7 - pos) * 8))
        Next

        Dim C As Long = &H1B
        Dim key1 As Long = key0 << 1
        If ((key0 And &H8000000000000000) <> 0) Then
            key1 = key1 Xor C
        End If

        Dim key2 As Long = key1 << 1
        If ((key1 And &H8000000000000000) <> 0) Then
            key2 = key2 Xor C
        End If

        Dim valor As Long

        For pos As Integer = 0 To 7
            valor = (key1 >> ((7 - pos) * 8)) And &HFF
            key1Out(pos) = valor
        Next


        For pos As Integer = 0 To 7
            valor = (key2 >> ((7 - pos) * 8)) And &HFF
            key2Out(pos) = valor
        Next

        Return Nothing
    End Function

    Private Function xorDataWithData(ByVal data1 As Byte(), ByVal data2 As Byte()) As Byte()

        Dim lenght As Integer = data1.Length
        Dim output(lenght - 1) As Byte

        If (data1.Length <> data2.Length) Then
            EscribirMensaje("error desiguales", "sredData: ", 1)
            Return output
        End If

        For pos As Integer = 0 To (lenght - 1)
            output(pos) = (data1(pos) Xor data2(pos)) And 255
        Next


        Return output
    End Function

    Private Function AreArraysEqual(Of T)(ByVal a As T(), ByVal b() As T) As Boolean

        'IF 2 NULL REFERENCES WERE PASSED IN, THEN RETURN TRUE, YOU MAY WANT TO RETURN FALSE
        If a Is Nothing AndAlso b Is Nothing Then Return True

        'CHECK THAT THERE IS NOT 1 NULL REFERENCE ARRAY
        If a Is Nothing Or b Is Nothing Then Return False

        'AT THIS POINT NEITHER ARRAY IS NULL
        'IF LENGTHS DON'T MATCH, THEY ARE NOT EQUAL
        If a.Length <> b.Length Then Return False

        'LOOP ARRAYS TO COMPARE CONTENTS
        For i As Integer = 0 To a.GetUpperBound(0)
            'RETURN FALSE AS SOON AS THERE IS NO MATCH
            If Not a(i).Equals(b(i)) Then Return False
        Next

        'IF WE GOT HERE, THE ARRAYS ARE EQUAL
        Return True

    End Function

    Private Function LeerXml(ByVal Comandos As String(), ByVal posicion As Integer, ByVal ruta As String) As String()
        Dim Aid As Boolean = False
        Dim BanderaLongitud As Boolean = False
        Dim Longitud As Integer = 0
        Dim comandoAuxiliar(4) As String
        inCantAid = 0
        inCantCapk = 0
        inCantAidAux = 0
        inCantCapkAux = 0

        Dim xmlReader As New XmlTextReader(ruta)
        While xmlReader.Read()
            Select Case xmlReader.NodeType

                Case XmlNodeType.Element
                    If (xmlReader.Name = "CONFIGURACION_EMV_AID" Or xmlReader.Name = "CONFIGURACION_EMV_CAPK") Then
                        Aid = True
                        posicion = posicion + 1

                        For pos1 As Integer = 0 To (posicion - 1)
                            comandoAuxiliar(pos1) = Comandos(pos1)
                        Next

                        ReDim Comandos(posicion)

                        For pos1 As Integer = 0 To posicion - 1
                            Comandos(pos1) = comandoAuxiliar(pos1)
                        Next

                        ReDim comandoAuxiliar(posicion)
                    End If

                    If (xmlReader.Name = "CONFIGURACION_EMV_AID") Then
                        inCantAid = inCantAid + 1
                        inCantAidAux = inCantAidAux + 1
                    End If
                    If (xmlReader.Name = "CONFIGURACION_EMV_CAPK") Then
                        inCantCapk = inCantCapk + 1
                        inCantCapkAux = inCantCapkAux + 1
                    End If
                    '---------------------------------------------------------------------
                    If (xmlReader.Name = "AplicationID" And Aid = True) Then
                        Comandos(posicion) = (Comandos(posicion) + "79,")
                        Longitud = Longitud + 1
                    End If
                    If (xmlReader.Name = "AplicationVersionNumber" And Aid = True) Then
                        Comandos(posicion) = (Comandos(posicion) + "159,09,")
                        Longitud = Longitud + 2
                    End If
                    If (xmlReader.Name = "DefaultAplicationName" And Aid = True) Then
                        Comandos(posicion) = (Comandos(posicion) + "80,")
                        Longitud = Longitud + 1
                    End If
                    If (xmlReader.Name = "ASI" And Aid = True) Then
                        Comandos(posicion) = (Comandos(posicion) + "223,32,")
                        Longitud = Longitud + 2
                    End If
                    If (xmlReader.Name = "MerchantCategoryCode" And Aid = True) Then
                        Comandos(posicion) = (Comandos(posicion) + "159,21,")
                        Longitud = Longitud + 2
                    End If
                    If (xmlReader.Name = "TerminalID" And Aid = True) Then
                        Comandos(posicion) = (Comandos(posicion) + "159,28,")
                        Longitud = Longitud + 2
                    End If
                    If (xmlReader.Name = "TerminalFloorLimit" And Aid = True) Then
                        Comandos(posicion) = (Comandos(posicion) + "159,27,")
                        Longitud = Longitud + 2
                    End If
                    If (xmlReader.Name = "ThresholdValue" And Aid = True) Then
                        Comandos(posicion) = (Comandos(posicion) + "223,36,")
                        Longitud = Longitud + 2
                    End If
                    If (xmlReader.Name = "TargetPercentage" And Aid = True) Then
                        Comandos(posicion) = (Comandos(posicion) + "223,37,")
                        Longitud = Longitud + 2
                    End If
                    If (xmlReader.Name = "MaximumTargetPercentage" And Aid = True) Then
                        Comandos(posicion) = (Comandos(posicion) + "223,38,")
                        Longitud = Longitud + 2
                    End If
                    If (xmlReader.Name = "TAC-Denial" And Aid = True) Then
                        Comandos(posicion) = (Comandos(posicion) + "223,33,")
                        Longitud = Longitud + 2
                    End If
                    If (xmlReader.Name = "TAC-Online" And Aid = True) Then
                        Comandos(posicion) = (Comandos(posicion) + "223,34,")
                        Longitud = Longitud + 2
                    End If
                    If (xmlReader.Name = "Write" And Aid = True) Then
                        Comandos(posicion) = (Comandos(posicion) + "223,102,")
                        Longitud = Longitud + 2
                    End If
                    If (xmlReader.Name = "TAC-Default" And Aid = True) Then
                        Comandos(posicion) = (Comandos(posicion) + "223,35,")
                        Longitud = Longitud + 2
                    End If
                    If (xmlReader.Name = "EMVAplication" And Aid = True) Then
                        Comandos(posicion) = (Comandos(posicion) + "223,45,")
                        Longitud = Longitud + 2
                    End If
                    If (xmlReader.Name = "DefaultTDOL" And Aid = True) Then
                        Comandos(posicion) = (Comandos(posicion) + "223,39,")
                        Longitud = Longitud + 2
                    End If
                    If (xmlReader.Name = "DefaultDDOL" And Aid = True) Then
                        Comandos(posicion) = (Comandos(posicion) + "223,40,")
                        Longitud = Longitud + 2
                    End If
                    If (xmlReader.Name = "MerchantID" And Aid = True) Then
                        Comandos(posicion) = (Comandos(posicion) + "159,22,")
                        Longitud = Longitud + 2
                    End If
                    If (xmlReader.Name = "AplicationFlowCapabilities" And Aid = True) Then
                        Comandos(posicion) = (Comandos(posicion) + "223,43,")
                        Longitud = Longitud + 2
                    End If
                    If (xmlReader.Name = "AditionalTags" And Aid = True) Then
                        Comandos(posicion) = (Comandos(posicion) + "223,41,")
                        Longitud = Longitud + 2
                    End If
                    If (xmlReader.Name = "MandatoryTags" And Aid = True) Then
                        Comandos(posicion) = (Comandos(posicion) + "223,42,")
                        Longitud = Longitud + 2
                    End If
                    If (xmlReader.Name = "KeyIndex" And Aid = True) Then
                        Comandos(posicion) = (Comandos(posicion) + "223,09,")
                        Longitud = Longitud + 2
                    End If
                    If (xmlReader.Name = "KeyData" And Aid = True) Then
                        Comandos(posicion) = (Comandos(posicion) + "223,11,129,")
                        Longitud = Longitud + 3
                    End If
                    If (xmlReader.Name = "RidAssociated" And Aid = True) Then
                        Comandos(posicion) = (Comandos(posicion) + "223,10,")
                        Longitud = Longitud + 2
                    End If
                    If (xmlReader.Name = "Checksum" And Aid = True) Then
                        Comandos(posicion) = (Comandos(posicion) + "223,12,")
                        Longitud = Longitud + 2
                    End If
                    If (xmlReader.Name = "Exponent" And Aid = True) Then
                        Comandos(posicion) = (Comandos(posicion) + "223,13,")
                        Longitud = Longitud + 2
                    End If
                    '---------------------------------------------------------------------
                    If (xmlReader.Name = "Longitud" And Aid = True) Then
                        BanderaLongitud = True
                    Else
                        BanderaLongitud = False
                    End If
                    Exit Select

                Case XmlNodeType.Text
                    If (BanderaLongitud = True) Then
                        Longitud = Longitud + xmlReader.Value + 1
                    End If
                    Comandos(posicion) = Comandos(posicion) + xmlReader.Value
                    Exit Select

                Case XmlNodeType.EndElement

                    If (xmlReader.Name = "CONFIGURACION_EMV_AID") Then
                        Dim comando2 As String = "02,65,00," & (Longitud + 15) & ",57,02,68,00,240,129," & Longitud & "," & Comandos(posicion)
                        Comandos(posicion) = comando2
                        Longitud = 0
                        Aid = False
                        BanderaLongitud = False
                    End If

                    If (xmlReader.Name = "CONFIGURACION_EMV_CAPK") Then
                        Dim comando2 As String = "02,65,00," & (Longitud + 15) & ",57,03,68,00,240,129," & Longitud & "," & Comandos(posicion)
                        Comandos(posicion) = comando2
                        Longitud = 0
                        Aid = False
                        BanderaLongitud = False
                    End If

                    Exit Select
            End Select
        End While

        Return Comandos
    End Function

    Private Function ConvertirPaquete(ByRef text As String) As Byte()
        Dim MensajeHexAux(1000) As Byte
        Dim MensajeHex As Byte()

        Dim bRetorno As Boolean
        Dim aux As String
        Dim i As Integer = 0
        Dim j As Integer
        Dim pos As Integer = 0

        bRetorno = False

        Do
            j = text.IndexOfAny(",", i)
            If (j > -1) Then
                bRetorno = True
                aux = text.Substring(i, j)
                MensajeHexAux(pos) = Integer.Parse(aux)
                If (text.Length >= j + 1) Then
                    text = text.Substring(j + 1)
                End If
            Else
                aux = text
                MensajeHexAux(pos) = Integer.Parse(aux)
                bRetorno = True
            End If
            pos = pos + 1
        Loop While (j > -1)

        ReDim MensajeHex(pos - 1)

        For pos1 As Integer = 0 To MensajeHex.Length - 1
            MensajeHex(pos1) = MensajeHexAux(pos1)
        Next

        Return MensajeHex
    End Function

    Private Function Autenticacion1Paso(ByRef MsjConc As String) As Byte
        Dim longitud As Integer = szMensaje.Length

        If (longitud > 42) Then
            EscribirMensaje("", "*----------Autenticacion Mutua Paso 1 Finalizada-----------*", 0)
            'EscribirMensaje(szMensaje, vbCrLf & "2) Respuesta Auth_Step_1 (HEX): ", 1)
            Enviar_Mensaje(Encoding.ASCII.GetBytes(ACK))

            Dim msjAuth1 As Byte() = authStep1(Encoding.Default.GetBytes(szMensaje))

            Dim resHexa() As Byte = Encoding.Default.GetBytes(szMensaje)
            Dim SW1 As Integer = resHexa(4)
            Dim SW2 As Integer = resHexa(5)

            If (SW1 = 144 And SW2 = 0) Then

                MsjConc = ""

                For pos As Integer = 0 To msjAuth1.Length - 1
                    MsjConc = MsjConc + String.Format("{0}{1}", ",", Convert.ToInt32(msjAuth1(pos)))
                Next

                'atuenticacion segundo paso
                EscribirMensaje("", "*----------Iniciando Autenticacion Mutua Paso 2------------*", 0)
                EscribirMensaje("", "*", 0)
                stComandos(1) = "02,65,00,36,32,17,00,02" + MsjConc + ",03"

                bBanderaComando = True
            Else
                bBanderaComando = False
            End If
        End If

        Return Nothing
    End Function

    Private Function Autenticacion2Paso() As Byte
        Dim longitud As Integer = szMensaje.Length

        If (longitud > 34) Then
            EscribirMensaje("", "*----------Autenticacion Mutua Paso 2 Finalizada-----------*", 0)
            EscribirMensaje("", "*", 0)
            'EscribirMensaje(szMensaje, vbCrLf & "2) Respuesta Auth_Step_2 (HEX): ", 1)
            Enviar_Mensaje(Encoding.ASCII.GetBytes(ACK))

            Dim msjAuth2 As Byte() = authStep2(Encoding.Default.GetBytes(szMensaje))
            Dim challenge(15) As Byte

            For pos As Integer = 0 To 15
                challenge(pos) = msjAuth2(pos)
                btJackSerial(pos) = msjAuth2(16 + pos)
            Next

            stSerialID = ""

            For pos As Integer = 1 To 10
                If (pos <> 3 And pos <> 7) Then
                    stSerialID = stSerialID + String.Format("{0}{1}", ",", Convert.ToInt32(btJackSerial(pos)))
                End If
            Next


            Dim bandera As Boolean = AreArraysEqual(challenge, btServerToken)

            If (bandera = True) Then
                EscribirMensaje("", "*----------AUNTENTICACION MUTUA EXITOSA--------------------*", 0)
                EscribirMensaje("", "*", 0)

                'EMV ON
                EscribirMensaje("", "*----------Iniciando Configuración EMV --------------------*", 0)
                EscribirMensaje("", "*", 0)
                stComandos(2) = "02,65,00,04,57,00,00,00,03"

                bBanderaComando = True
            Else
                bBanderaComando = False
            End If

        End If

        Return Nothing
    End Function

    Private Function EMV_ON() As Byte

        If (szMensaje.Length > 0) Then
            
            'EscribirMensaje(szMensaje, vbCrLf & "2) Respuesta EMV ON (HEX): ", 1)

            Dim resHexa() As Byte = Encoding.Default.GetBytes(szMensaje)
            Dim SW1 As Integer = resHexa(4)
            Dim SW2 As Integer = resHexa(5)
            Enviar_Mensaje(Encoding.ASCII.GetBytes(ACK))

            If (SW1 = 144 And SW2 = 0) Then

                EscribirMensaje("", "*----------Inicio de la Configuración EMV Exitosa----------*", 0)
                EscribirMensaje("", "*", 0)



                Dim cmdCmac As Byte() = getCMACForData1(btJackSerial)

                Dim cmacString As String = ""
                For pos As Integer = 0 To 7
                    cmacString = cmacString + String.Format("{0}{1}", ",", Convert.ToInt32(cmdCmac(pos)))
                Next

                If (blTipoProceso = False) Then
                    EscribirMensaje("", "*----------Iniciando Configuracion EMV del Terminal--------*", 0)
                    EscribirMensaje("", "*", 0)
                    stComandos(3) = "2,65,0,57,57,1,68,0,240,43,95,54,1,2,95,42,2,9,55,159,51,3,224,248,200,159,53,1,34,159,64,5,240,0,176,160,1,159,26,2,8,98,159,30,8" + stSerialID + "" + cmacString + ",3"
                End If
                bBanderaComando = True
            Else
                bBanderaComando = False
            End If

        End If

        Return Nothing
    End Function

    Private Function CONF_TERM() As Integer

        If (szMensaje.Length > 0) Then

            'EscribirMensaje(szMensaje, vbCrLf & "2) Respuesta EMV conf terminal (HEX): ", 1)

            Dim resHexa() As Byte = Encoding.Default.GetBytes(szMensaje)

            Dim SW1 As Integer = resHexa(4)
            Dim SW2 As Integer = resHexa(5)
            Enviar_Mensaje(Encoding.ASCII.GetBytes(ACK))


            If (SW1 = 144 And SW2 = 0) Then '90 00

                EscribirMensaje("", "*----------Configuración EMV del terminal Exitosa----------*", 0)
                EscribirMensaje("", "*", 0)

                stComandos = LeerXml(stComandos, 3, strPath)
                inCantRegistros = stComandos.Length - 4

                For pos As Integer = 4 To 3 + inCantRegistros
                    Dim comandoAux As String = stComandos(pos)
                    Dim comandoHexa As Byte() = ConvertirPaquete(comandoAux)
                    Dim cmdCmac As Byte() = getCMACForData2(comandoHexa)
                    Dim cmacString As String = ""

                    For pos1 As Integer = 0 To 7
                        cmacString = cmacString + String.Format("{0}{1}", ",", Convert.ToInt32(cmdCmac(pos1)))
                    Next

                    inContadorComandos = inContadorComandos + 1

                    stComandos(pos) = stComandos(pos) + "" + cmacString + ",3"
                    bBanderaComando = True
                Next

                EscribirMensaje("", "*Iniciando Configuración de los Aid's y CAPK's del terminal*", 0)
                EscribirMensaje("", "*", 0)
            Else
                bBanderaComando = False
            End If
        End If

        Return Nothing
    End Function

    Private Function CONF_AID_CAPK() As Integer
        If (szMensaje.Length > 0) Then

            'EscribirMensaje(szMensaje, vbCrLf & "2) Respuesta EMV conf aid (HEX): ", 1)

            Dim resHexa() As Byte = Encoding.Default.GetBytes(szMensaje)

            Dim SW1 As Integer = resHexa(4)
            Dim SW2 As Integer = resHexa(5)
            Enviar_Mensaje(Encoding.ASCII.GetBytes(ACK))

            If (SW1 = 144 And SW2 = 0) Then '90 00

                If (inCantCapkAux <> 0 And inCantAidAux = 0) Then
                    EscribirMensaje("", "*----------CAPK N° " & (inCantCapk - (inCantCapkAux - 1)) & " Configurada Correctamente ------------*", 0)
                    EscribirMensaje("", "*", 0)
                    inCantCapkAux = inCantCapkAux - 1
                End If

                If (inCantAidAux <> 0) Then
                    EscribirMensaje("", "*----------Aid N° " & (inCantAid - (inCantAidAux - 1)) & " Configurada Correctamente -------------*", 0)
                    EscribirMensaje("", "*", 0)
                    inCantAidAux = inCantAidAux - 1
                End If

                
            End If


            If (SW1 = 144 And SW2 = 0 And (inPosComando = (3 + inCantRegistros))) Then '90 00

                inContadorComandos = inContadorComandos + 1

                Dim comandoAuxiliar((3 + inCantRegistros)) As String

                For pos1 As Integer = 0 To ((inPosComando + 1) - 1)
                    comandoAuxiliar(pos1) = stComandos(pos1)
                Next

                ReDim stComandos(inPosComando + 1)

                For pos1 As Integer = 0 To inPosComando
                    stComandos(pos1) = comandoAuxiliar(pos1)
                Next

                ReDim comandoAuxiliar(inPosComando + 1)
                stComandos(inPosComando + 1) = "02,65,00,04,57,00,00,01,03"
                EscribirMensaje("", "*----------Finalizando Configuracion EMV ------------------*", 0)
                EscribirMensaje("", "*", 0)
            End If

        End If

        Return Nothing
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCargarXml.Click
        Dim openFile As New OpenFileDialog
        openFile.Filter = "Archivos XML Conf EMV|*.xml"

        If openFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim i As Integer
            i = openFile.FileName.LastIndexOf("")
            strPath = openFile.FileName
        End If

        EscribirMensaje("", vbCrLf & "Ruta del Archivo de Configuracion: " & strPath, 1)

    End Sub

    Private Sub ButtonLimpiarAids_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonLimpiarAids.Click
        ButtonLimpiarAids.Enabled = False

        Dim szPaquete As String = String.Empty
        Dim inIntentos As Integer
        Dim CRC1, CRC2 As Integer
        Dim data2 As [Byte]()
        Dim data1 As [Byte]()
        Dim btMensaje() As [Byte]
        Dim bLogico As Boolean
        Dim rnd As New Random()
        Dim MsjConc As String = ""
        blTipoProceso = True
        '----------------------------------------------------------------------------------------
        inPosComando = 0
        bBanderaComando = True
        inContadorComandos = 3
        '----------------------------------------------------------------------------------------'
        CargarParametrosPuerto()
        AbrirPuerto()
        '----------------------------------------------------------------------------------------'
        rnd.NextBytes(btServerRandom)
        rnd.NextBytes(btServerToken)

        For pos As Integer = 0 To btServerRandom.GetUpperBound(0)
            MsjConc = MsjConc + String.Format("{0}{1}", ",", Convert.ToInt32(btServerRandom(pos)))
        Next
        'atuenticacion primer paso
        stComandos(0) = "02,65,00,20,32,17,00,01" + MsjConc + ",03"
        '----------------------------------------------------------------------------------------'

        Do
            bLogico = ArmarPaquete(szPaquete, stComandos(inPosComando))
            'Console.Write("!" & vbCrLf)

            If szPaquete.Length = 0 Then
                Exit Sub
            End If

            CRC1 = CalcularCRC1(stComandos(inPosComando))
            CRC2 = CalcularCRC2(stComandos(inPosComando))

            If (bLogico = True) Then

                data1 = Encoding.Default.GetBytes(szPaquete)
                data2 = {CRC1, CRC2}
                ReDim btMensaje(data1.Length + data2.Length)
                data1.CopyTo(btMensaje, 0)
                data2.CopyTo(btMensaje, data1.Length)
                ' EscribirMensaje(stComandos(inPosComando), vbCrLf & " MSJ ENVIADO: ", 0)

                'Enviando el mensaje a traves del puerto serial
                If (Enviar_Mensaje(btMensaje) = False) Then
                    Exit Sub
                End If

                FechaInactivo = Now
                szMensaje = ""

                '15 segundos es el tiempo estándar que se debe esperar por un ACK/NAK/EOT
                While (Check_TimeOut(15) = False) And (szMensaje.Length = 0)
                    Recibir_ACK_NAK_EOT()
                End While


                If (szMensaje.Length > 0) Then
                    'EscribirMensaje(szMensaje, vbCrLf & "1) Recibió (HEX): ", 1)
                End If

                If (szMensaje.Length > 0) And (szMensaje = ACK) Then
                    ' Valida si se recibe respuesta
                    inIntentos = 0

                    Do
                        FechaInactivo = Now
                        szMensaje = ""
                        bReadRS232 = False

                        Do
                            RecibirMensaje_STX_ETX()
                        Loop While (Check_TimeOut(1) = False) And (bReadRS232 = False)

                        inIntentos = inIntentos + 1

                        '---------------------------------------------------------------------------------
                        If (inPosComando = 0) Then
                            If (szMensaje.Length > 42) Then
                                Autenticacion1Paso(MsjConc)
                                inIntentos = 3
                            ElseIf (inIntentos = 3) Then
                                inPosComando = inContadorComandos + 1
                            End If
                        ElseIf (inPosComando = 1) Then
                            If (szMensaje.Length > 34) Then
                                Autenticacion2Paso()
                                inIntentos = 3
                            ElseIf (inIntentos = 3) Then
                                inPosComando = inContadorComandos + 1
                            End If
                        ElseIf (inPosComando = 2) Then
                            If (szMensaje.Length > 6) Then
                                EMV_ON()
                                If (bBanderaComando = True) Then
                                    Dim comandoAux As String = "02,65,00,19,57,02,68,00,240,129,04,223,102,01,32"
                                    Dim comandoHexa As Byte() = ConvertirPaquete(comandoAux)
                                    Dim cmdCmac As Byte() = getCMACForData2(comandoHexa)
                                    Dim cmacString As String = ""

                                    For pos1 As Integer = 0 To 7
                                        cmacString = cmacString + String.Format("{0}{1}", ",", Convert.ToInt32(cmdCmac(pos1)))
                                    Next

                                    stComandos(3) = "02,65,00,19,57,02,68,00,240,129,04,223,102,01,32" + cmacString + ",3"
                                    bBanderaComando = True
                                End If
                                inIntentos = 3
                            ElseIf (inIntentos = 3) Then
                                inPosComando = inContadorComandos + 1
                            End If
                        ElseIf (inPosComando = 3) Then
                            If (szMensaje.Length > 6) Then
                                ' EscribirMensaje(szMensaje, vbCrLf & "2) Respuesta limpiar (HEX): ", 1)

                                Dim resHexa() As Byte = Encoding.Default.GetBytes(szMensaje)

                                Dim SW1 As Integer = resHexa(4)
                                Dim SW2 As Integer = resHexa(5)
                                Enviar_Mensaje(Encoding.ASCII.GetBytes(ACK))

                                If (SW1 = 144 And SW2 = 0) Then '90 00
                                    EscribirMensaje("", "*----------AID's Eliminados Satisfactoriamente ------------*", 0)
                                    EscribirMensaje("", "*", 0)

                                    bBanderaComando = True
                                    stComandos(4) = "02,65,00,04,57,00,00,01,03"
                                End If
                                inIntentos = 3
                            ElseIf (inIntentos = 3) Then
                                inPosComando = inContadorComandos + 1
                            End If
                        ElseIf (inPosComando = 4) Then
                            If (szMensaje.Length > 6) Then
                                EscribirMensaje("", "*--------Finalizacion Configuracion EMV Satisfactoriamente-*", 0)
                                EscribirMensaje("", "*", 0)
                                'EscribirMensaje(szMensaje, vbCrLf & "2) Respuesta emv off (HEX): ", 1)
                                bBanderaComando = True
                                inIntentos = 3
                            End If
                        End If
                        '-----------------------------------------------------------------------
                    Loop Until (inIntentos = 3)
                End If
            End If

            If (bBanderaComando = True) Then
                inPosComando = inPosComando + 1
            End If
            bBanderaComando = False
        Loop While (inPosComando < 5)

        CerrarPuerto()

        ButtonLimpiarAids.Enabled = True

    End Sub

    Private Sub ButtonLimpiarCAPK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonLimpiarCAPK.Click
        ButtonLimpiarCAPK.Enabled = False

        Dim szPaquete As String = String.Empty
        Dim inIntentos As Integer
        Dim CRC1, CRC2 As Integer
        Dim data2 As [Byte]()
        Dim data1 As [Byte]()
        Dim btMensaje() As [Byte]
        Dim bLogico As Boolean
        Dim rnd As New Random()
        Dim MsjConc As String = ""
        blTipoProceso = True
        '----------------------------------------------------------------------------------------
        inPosComando = 0
        bBanderaComando = True
        inContadorComandos = 3
        '----------------------------------------------------------------------------------------'
        CargarParametrosPuerto()
        AbrirPuerto()
        '----------------------------------------------------------------------------------------'
        rnd.NextBytes(btServerRandom)
        rnd.NextBytes(btServerToken)

        For pos As Integer = 0 To btServerRandom.GetUpperBound(0)
            MsjConc = MsjConc + String.Format("{0}{1}", ",", Convert.ToInt32(btServerRandom(pos)))
        Next
        'atuenticacion primer paso
        stComandos(0) = "02,65,00,20,32,17,00,01" + MsjConc + ",03"
        '----------------------------------------------------------------------------------------'

        Do
            bLogico = ArmarPaquete(szPaquete, stComandos(inPosComando))
            'Console.Write("!" & vbCrLf)

            If szPaquete.Length = 0 Then
                Exit Sub
            End If

            CRC1 = CalcularCRC1(stComandos(inPosComando))
            CRC2 = CalcularCRC2(stComandos(inPosComando))

            If (bLogico = True) Then

                data1 = Encoding.Default.GetBytes(szPaquete)
                data2 = {CRC1, CRC2}
                ReDim btMensaje(data1.Length + data2.Length)
                data1.CopyTo(btMensaje, 0)
                data2.CopyTo(btMensaje, data1.Length)
                'EscribirMensaje(stComandos(inPosComando), vbCrLf & " MSJ ENVIADO: ", 0)

                'Enviando el mensaje a traves del puerto serial
                If (Enviar_Mensaje(btMensaje) = False) Then
                    Exit Sub
                End If

                FechaInactivo = Now
                szMensaje = ""

                '15 segundos es el tiempo estándar que se debe esperar por un ACK/NAK/EOT
                While (Check_TimeOut(15) = False) And (szMensaje.Length = 0)
                    Recibir_ACK_NAK_EOT()
                End While


                If (szMensaje.Length > 0) Then
                    ' EscribirMensaje(szMensaje, vbCrLf & "1) Recibió (HEX): ", 1)
                End If

                If (szMensaje.Length > 0) And (szMensaje = ACK) Then
                    ' Valida si se recibe respuesta
                    inIntentos = 0

                    Do
                        FechaInactivo = Now
                        szMensaje = ""
                        bReadRS232 = False

                        Do
                            RecibirMensaje_STX_ETX()
                        Loop While (Check_TimeOut(1) = False) And (bReadRS232 = False)

                        inIntentos = inIntentos + 1

                        '---------------------------------------------------------------------------------
                        If (inPosComando = 0) Then
                            If (szMensaje.Length > 42) Then
                                Autenticacion1Paso(MsjConc)
                                inIntentos = 3
                            ElseIf (inIntentos = 3) Then
                                inPosComando = inContadorComandos + 1
                            End If
                        ElseIf (inPosComando = 1) Then
                            If (szMensaje.Length > 34) Then
                                Autenticacion2Paso()
                                inIntentos = 3
                            ElseIf (inIntentos = 3) Then
                                inPosComando = inContadorComandos + 1
                            End If
                        ElseIf (inPosComando = 2) Then
                            If (szMensaje.Length > 6) Then
                                EMV_ON()
                                If (bBanderaComando = True) Then
                                    Dim comandoAux As String = "02,65,00,19,57,03,68,00,240,129,04,223,102,01,32"
                                    Dim comandoHexa As Byte() = ConvertirPaquete(comandoAux)
                                    Dim cmdCmac As Byte() = getCMACForData2(comandoHexa)
                                    Dim cmacString As String = ""

                                    For pos1 As Integer = 0 To 7
                                        cmacString = cmacString + String.Format("{0}{1}", ",", Convert.ToInt32(cmdCmac(pos1)))
                                    Next

                                    stComandos(3) = "02,65,00,19,57,02,68,00,240,129,04,223,102,01,32" + cmacString + ",3"
                                    bBanderaComando = True
                                End If
                                inIntentos = 3
                            ElseIf (inIntentos = 3) Then
                                inPosComando = inContadorComandos + 1
                            End If
                        ElseIf (inPosComando = 3) Then
                            If (szMensaje.Length > 6) Then
                                'EscribirMensaje(szMensaje, vbCrLf & "2) Respuesta limpiar capk(HEX): ", 1)

                                Dim resHexa() As Byte = Encoding.Default.GetBytes(szMensaje)

                                Dim SW1 As Integer = resHexa(4)
                                Dim SW2 As Integer = resHexa(5)
                                Enviar_Mensaje(Encoding.ASCII.GetBytes(ACK))

                                If (SW1 = 144 And SW2 = 0) Then '90 00
                                    EscribirMensaje("", "*----------CAPK's Eliminados Satisfactoriamente -----------*", 0)
                                    bBanderaComando = True
                                    stComandos(4) = "02,65,00,04,57,00,00,01,03"
                                End If
                                inIntentos = 3
                            ElseIf (inIntentos = 3) Then
                                inPosComando = inContadorComandos + 1
                            End If
                        ElseIf (inPosComando = 4) Then
                            If (szMensaje.Length > 0) Then
                                EscribirMensaje("", "*--------Finalizacion Configuracion EMV Satisfactoriamente-*", 0)
                                EscribirMensaje("", "*", 0)
                                'EscribirMensaje(szMensaje, vbCrLf & "2) Respuesta emv off (HEX): ", 1)
                                bBanderaComando = True
                                inIntentos = 3
                            End If
                        End If
                        '-----------------------------------------------------------------------
                    Loop Until (inIntentos = 3)
                End If
            End If

            If (bBanderaComando = True) Then
                inPosComando = inPosComando + 1
            End If
            bBanderaComando = False
        Loop While (inPosComando < 5)

        CerrarPuerto()

        ButtonLimpiarCAPK.Enabled = True


    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim mensaje As New frmMensaje
        mensaje.ShowDialog()

        If DialogResult.Yes Then
            Exit Sub
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim loPSI As New ProcessStartInfo
        Dim loProceso As New Process

        loPSI.FileName = "LIN_ESPS Configuracion EMV E105 Linkit.pdf"

        Try
            loProceso = Process.Start(loPSI)
        Catch Exp As Exception
            MessageBox.Show(Exp.Message, "Se produjo un error, consulte con soporte", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

#End Region

    
End Class
