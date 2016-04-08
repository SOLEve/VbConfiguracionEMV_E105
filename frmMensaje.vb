Public Class frmMensaje
    Inherits System.Windows.Forms.Form

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents btnMensaje As System.Windows.Forms.Button
    Friend WithEvents lblMensaje As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents pctLogodelSistema As System.Windows.Forms.PictureBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMensaje))
        Me.btnMensaje = New System.Windows.Forms.Button()
        Me.lblMensaje = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.pctLogodelSistema = New System.Windows.Forms.PictureBox()
        CType(Me.pctLogodelSistema, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnMensaje
        '
        Me.btnMensaje.DialogResult = System.Windows.Forms.DialogResult.Yes
        Me.btnMensaje.Location = New System.Drawing.Point(442, 157)
        Me.btnMensaje.Name = "btnMensaje"
        Me.btnMensaje.Size = New System.Drawing.Size(115, 37)
        Me.btnMensaje.TabIndex = 56
        Me.btnMensaje.Text = "Volver"
        '
        'lblMensaje
        '
        Me.lblMensaje.AutoSize = True
        Me.lblMensaje.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMensaje.ForeColor = System.Drawing.Color.Navy
        Me.lblMensaje.Location = New System.Drawing.Point(140, 111)
        Me.lblMensaje.Name = "lblMensaje"
        Me.lblMensaje.Size = New System.Drawing.Size(317, 18)
        Me.lblMensaje.TabIndex = 57
        Me.lblMensaje.Text = "Versión : LIN - LIN VQT.000.000 01.01.01"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(248, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 17)
        Me.Label1.TabIndex = 106
        Me.Label1.Text = "Acerca de:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Navy
        Me.Label2.Location = New System.Drawing.Point(106, 83)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(278, 18)
        Me.Label2.TabIndex = 107
        Me.Label2.Text = "Aplicacion Configuracion EMV E105"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(198, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(179, 17)
        Me.Label3.TabIndex = 108
        Me.Label3.Text = "Linkit Ingeniería y Sistemas"
        '
        'pctLogodelSistema
        '
        Me.pctLogodelSistema.BackColor = System.Drawing.Color.Transparent
        Me.pctLogodelSistema.Image = CType(resources.GetObject("pctLogodelSistema.Image"), System.Drawing.Image)
        Me.pctLogodelSistema.Location = New System.Drawing.Point(19, 9)
        Me.pctLogodelSistema.Name = "pctLogodelSistema"
        Me.pctLogodelSistema.Size = New System.Drawing.Size(58, 56)
        Me.pctLogodelSistema.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pctLogodelSistema.TabIndex = 109
        Me.pctLogodelSistema.TabStop = False
        '
        'frmMensaje
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(558, 204)
        Me.Controls.Add(Me.lblMensaje)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.pctLogodelSistema)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnMensaje)
        Me.Controls.Add(Me.Label2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(576, 249)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(576, 249)
        Me.Name = "frmMensaje"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Linkit Ingeniería y Sistemas"
        CType(Me.pctLogodelSistema, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region


End Class
