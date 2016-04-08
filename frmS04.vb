Public Class frmS04
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
    Friend WithEvents btnS04 As System.Windows.Forms.Button
    Friend WithEvents txtCoord_X1 As System.Windows.Forms.TextBox
    Friend WithEvents txtCoord_Y1 As System.Windows.Forms.TextBox
    Friend WithEvents txtCoord_X2 As System.Windows.Forms.TextBox
    Friend WithEvents txtCoord_Y2 As System.Windows.Forms.TextBox
    Friend WithEvents txtLimpia_Pantalla As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.btnS04 = New System.Windows.Forms.Button
        Me.txtCoord_X1 = New System.Windows.Forms.TextBox
        Me.txtCoord_Y1 = New System.Windows.Forms.TextBox
        Me.txtCoord_X2 = New System.Windows.Forms.TextBox
        Me.txtCoord_Y2 = New System.Windows.Forms.TextBox
        Me.txtLimpia_Pantalla = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Navy
        Me.Label1.Location = New System.Drawing.Point(8, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(312, 32)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Coordenada de inicio en el eje x de la pantalla para definición de la región de c" & _
        "aptura (000-320)"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Navy
        Me.Label2.Location = New System.Drawing.Point(8, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(312, 32)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Coordenada de inicio en el eje y de la pantalla para definición de la región de c" & _
        "aptura (000-220)"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Navy
        Me.Label3.Location = New System.Drawing.Point(9, 109)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(312, 32)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Coordenada de fin en el eje x de la pantalla para definición de la región de capt" & _
        "ura (320-x)"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Navy
        Me.Label4.Location = New System.Drawing.Point(8, 160)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(312, 32)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Coordenada de fin en el eje y de la pantalla para definición de la región de capt" & _
        "ura (220-y)"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Navy
        Me.Label5.Location = New System.Drawing.Point(8, 208)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(312, 32)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Indica si la pantalla debe ser limpiada al detectar que se comience a firmar (0-1" & _
        ")"
        '
        'btnS04
        '
        Me.btnS04.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.btnS04.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnS04.Location = New System.Drawing.Point(128, 272)
        Me.btnS04.Name = "btnS04"
        Me.btnS04.Size = New System.Drawing.Size(136, 24)
        Me.btnS04.TabIndex = 17
        Me.btnS04.Text = "&Aceptar"
        '
        'txtCoord_X1
        '
        Me.txtCoord_X1.Location = New System.Drawing.Point(352, 24)
        Me.txtCoord_X1.MaxLength = 3
        Me.txtCoord_X1.Name = "txtCoord_X1"
        Me.txtCoord_X1.Size = New System.Drawing.Size(32, 20)
        Me.txtCoord_X1.TabIndex = 18
        Me.txtCoord_X1.Text = ""
        '
        'txtCoord_Y1
        '
        Me.txtCoord_Y1.Location = New System.Drawing.Point(352, 64)
        Me.txtCoord_Y1.MaxLength = 3
        Me.txtCoord_Y1.Name = "txtCoord_Y1"
        Me.txtCoord_Y1.Size = New System.Drawing.Size(32, 20)
        Me.txtCoord_Y1.TabIndex = 19
        Me.txtCoord_Y1.Text = ""
        '
        'txtCoord_X2
        '
        Me.txtCoord_X2.Location = New System.Drawing.Point(352, 109)
        Me.txtCoord_X2.MaxLength = 3
        Me.txtCoord_X2.Name = "txtCoord_X2"
        Me.txtCoord_X2.Size = New System.Drawing.Size(32, 20)
        Me.txtCoord_X2.TabIndex = 20
        Me.txtCoord_X2.Text = ""
        '
        'txtCoord_Y2
        '
        Me.txtCoord_Y2.Location = New System.Drawing.Point(352, 160)
        Me.txtCoord_Y2.MaxLength = 3
        Me.txtCoord_Y2.Name = "txtCoord_Y2"
        Me.txtCoord_Y2.Size = New System.Drawing.Size(32, 20)
        Me.txtCoord_Y2.TabIndex = 21
        Me.txtCoord_Y2.Text = ""
        '
        'txtLimpia_Pantalla
        '
        Me.txtLimpia_Pantalla.Location = New System.Drawing.Point(352, 208)
        Me.txtLimpia_Pantalla.MaxLength = 1
        Me.txtLimpia_Pantalla.Name = "txtLimpia_Pantalla"
        Me.txtLimpia_Pantalla.Size = New System.Drawing.Size(32, 20)
        Me.txtLimpia_Pantalla.TabIndex = 22
        Me.txtLimpia_Pantalla.Text = ""
        '
        'frmS04
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ClientSize = New System.Drawing.Size(448, 326)
        Me.ControlBox = False
        Me.Controls.Add(Me.txtLimpia_Pantalla)
        Me.Controls.Add(Me.txtCoord_Y2)
        Me.Controls.Add(Me.txtCoord_X2)
        Me.Controls.Add(Me.txtCoord_Y1)
        Me.Controls.Add(Me.txtCoord_X1)
        Me.Controls.Add(Me.btnS04)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmS04"
        Me.Text = "Datos del Mensaje S04"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnS04_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnS04.Click
        Coord_X1 = txtCoord_X1.Text
        Coord_X2 = txtCoord_X2.Text
        Coord_Y1 = txtCoord_Y1.Text
        Coord_Y2 = txtCoord_Y2.Text
        Limpia_Pantalla = txtLimpia_Pantalla.Text

        Me.Close()
        frmFirmas.Show()

    End Sub

    Private Sub frmS04_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtCoord_X1.Text = Coord_X1
        txtCoord_X2.Text = Coord_X2
        txtCoord_Y1.Text = Coord_Y1
        txtCoord_Y2.Text = Coord_Y2
        txtLimpia_Pantalla.Text = Limpia_Pantalla

    End Sub
End Class
