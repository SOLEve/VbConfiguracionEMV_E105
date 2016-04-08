Public Class frmS03
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnS03 As System.Windows.Forms.Button
    Friend WithEvents txttime1 As System.Windows.Forms.TextBox
    Friend WithEvents txttime2 As System.Windows.Forms.TextBox
    Friend WithEvents txtmodo_des As System.Windows.Forms.TextBox
    Friend WithEvents txtmodo_alm As System.Windows.Forms.TextBox
    Friend WithEvents txtresol As System.Windows.Forms.TextBox
    Friend WithEvents txtformato_img As System.Windows.Forms.TextBox
    Friend WithEvents txtflash As System.Windows.Forms.TextBox
    Friend WithEvents txtname_file As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.txttime1 = New System.Windows.Forms.TextBox
        Me.txttime2 = New System.Windows.Forms.TextBox
        Me.txtmodo_des = New System.Windows.Forms.TextBox
        Me.txtmodo_alm = New System.Windows.Forms.TextBox
        Me.txtresol = New System.Windows.Forms.TextBox
        Me.txtformato_img = New System.Windows.Forms.TextBox
        Me.txtflash = New System.Windows.Forms.TextBox
        Me.txtname_file = New System.Windows.Forms.TextBox
        Me.btnS03 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Navy
        Me.Label1.Location = New System.Drawing.Point(8, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(312, 32)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Tiempo en seg. que la aplicación debe esperar para que el usuario comience a firm" & _
        "ar (0-300):"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Navy
        Me.Label2.Location = New System.Drawing.Point(8, 67)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(312, 32)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Tiempo en seg. que la aplicación debe esperar después que el usuario termine de f" & _
        "irmar (0-300):"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Navy
        Me.Label3.Location = New System.Drawing.Point(8, 112)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(312, 32)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Modo de Despliegue (0-1):"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Navy
        Me.Label4.Location = New System.Drawing.Point(8, 152)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(312, 32)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Modo de almacenamiento de los datos de la imagen (0-1):"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Navy
        Me.Label5.Location = New System.Drawing.Point(8, 192)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(312, 32)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Resolución de la imagen de la firma (000-999):"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Navy
        Me.Label6.Location = New System.Drawing.Point(8, 232)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(312, 32)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Formato de almacenamiento de la imagen (0-6):"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Navy
        Me.Label7.Location = New System.Drawing.Point(8, 272)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(312, 32)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Indica si la imagen capturada de la firma debe ser guardada o no en memoria Flash" & _
        " (0-1):"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Navy
        Me.Label8.Location = New System.Drawing.Point(8, 320)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(312, 32)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Nombre del archivo que será utilizado para guardar la imagen :"
        '
        'txttime1
        '
        Me.txttime1.Location = New System.Drawing.Point(336, 24)
        Me.txttime1.MaxLength = 3
        Me.txttime1.Name = "txttime1"
        Me.txttime1.Size = New System.Drawing.Size(32, 20)
        Me.txttime1.TabIndex = 8
        Me.txttime1.Text = ""
        '
        'txttime2
        '
        Me.txttime2.Location = New System.Drawing.Point(336, 66)
        Me.txttime2.MaxLength = 3
        Me.txttime2.Name = "txttime2"
        Me.txttime2.Size = New System.Drawing.Size(32, 20)
        Me.txttime2.TabIndex = 9
        Me.txttime2.Text = ""
        '
        'txtmodo_des
        '
        Me.txtmodo_des.Location = New System.Drawing.Point(336, 108)
        Me.txtmodo_des.MaxLength = 1
        Me.txtmodo_des.Name = "txtmodo_des"
        Me.txtmodo_des.Size = New System.Drawing.Size(32, 20)
        Me.txtmodo_des.TabIndex = 10
        Me.txtmodo_des.Text = ""
        '
        'txtmodo_alm
        '
        Me.txtmodo_alm.Location = New System.Drawing.Point(336, 150)
        Me.txtmodo_alm.MaxLength = 1
        Me.txtmodo_alm.Name = "txtmodo_alm"
        Me.txtmodo_alm.Size = New System.Drawing.Size(32, 20)
        Me.txtmodo_alm.TabIndex = 11
        Me.txtmodo_alm.Text = ""
        '
        'txtresol
        '
        Me.txtresol.Location = New System.Drawing.Point(336, 192)
        Me.txtresol.MaxLength = 3
        Me.txtresol.Name = "txtresol"
        Me.txtresol.Size = New System.Drawing.Size(32, 20)
        Me.txtresol.TabIndex = 12
        Me.txtresol.Text = ""
        '
        'txtformato_img
        '
        Me.txtformato_img.Location = New System.Drawing.Point(336, 234)
        Me.txtformato_img.MaxLength = 1
        Me.txtformato_img.Name = "txtformato_img"
        Me.txtformato_img.Size = New System.Drawing.Size(32, 20)
        Me.txtformato_img.TabIndex = 13
        Me.txtformato_img.Text = ""
        '
        'txtflash
        '
        Me.txtflash.Location = New System.Drawing.Point(336, 276)
        Me.txtflash.MaxLength = 1
        Me.txtflash.Name = "txtflash"
        Me.txtflash.Size = New System.Drawing.Size(32, 20)
        Me.txtflash.TabIndex = 14
        Me.txtflash.Text = ""
        '
        'txtname_file
        '
        Me.txtname_file.Location = New System.Drawing.Point(336, 318)
        Me.txtname_file.MaxLength = 6
        Me.txtname_file.Name = "txtname_file"
        Me.txtname_file.Size = New System.Drawing.Size(96, 20)
        Me.txtname_file.TabIndex = 15
        Me.txtname_file.Text = ""
        '
        'btnS03
        '
        Me.btnS03.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.btnS03.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnS03.Location = New System.Drawing.Point(144, 360)
        Me.btnS03.Name = "btnS03"
        Me.btnS03.Size = New System.Drawing.Size(136, 24)
        Me.btnS03.TabIndex = 16
        Me.btnS03.Text = "&Aceptar"
        '
        'frmS03
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ClientSize = New System.Drawing.Size(456, 406)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnS03)
        Me.Controls.Add(Me.txtname_file)
        Me.Controls.Add(Me.txtflash)
        Me.Controls.Add(Me.txtformato_img)
        Me.Controls.Add(Me.txtresol)
        Me.Controls.Add(Me.txtmodo_alm)
        Me.Controls.Add(Me.txtmodo_des)
        Me.Controls.Add(Me.txttime2)
        Me.Controls.Add(Me.txttime1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmS03"
        Me.Text = "Datos de Mensaje S03"
        Me.ResumeLayout(False)

    End Sub

#End Region
    Public inClose As Integer
    Private Sub frmS03_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        inClose = 0
        txttime1.Text = time1
        txttime2.Text = time2
        txtmodo_des.Text = modo_despliegue
        txtmodo_alm.Text = modo_almacenamiento
        txtresol.Text = resolucion
        txtformato_img.Text = formato_imagen
        txtflash.Text = almacena_flash
        txtname_file.Text = nombre_archivo


    End Sub

    Private Sub btnS03_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnS03.Click
        time1 = txttime1.Text
        time2 = txttime2.Text
        modo_despliegue = txtmodo_des.Text
        modo_almacenamiento = txtmodo_alm.Text
        resolucion = txtresol.Text
        formato_imagen = txtformato_img.Text
        almacena_flash = txtflash.Text
        nombre_archivo = txtname_file.Text
        inClose = 1

        Me.Close()
        'Form1.Show()
        frmFirmas.Show()

    End Sub
End Class
