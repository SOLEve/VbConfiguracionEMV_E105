Module Variab
    Public Const CONST_ENTER As String = Chr(13) + Chr(10)

    Public Const STX As Char = Chr(2)
    Public Const ETX As Char = Chr(3)
    Public Const EOT As Char = Chr(4)
    Public Const ACK As Char = Chr(6)
    Public Const NAK As Char = Chr(21)
    Public Const SI As Char = Chr(15)
    Public Const SO As Char = Chr(14)

    ' Variable para manejar el timer de inactividad
    Public FechaInactivo As Date
    Public inDuracionTimerSegundos As Integer = 1
    Public Const PAD_RIGHT As Integer = 1
    Public Const PAD_LEFT As Integer = 2

    'Variables Usadas para armar el Mensaje S03
    Public time1 As String
    Public time2 As String
    Public modo_despliegue As String
    Public modo_almacenamiento As String
    Public resolucion As String
    Public formato_imagen As String
    Public almacena_flash As String
    Public nombre_archivo As String

    'Variables Usadas para armar el Mensaje S04
    Public Coord_X1 As String
    Public Coord_X2 As String
    Public Coord_Y1 As String
    Public Coord_Y2 As String
    Public Limpia_Pantalla As String

    'Llaves Dukpt
    Public btEngineID() As Byte
    Public btKsnengineID() As Byte
    Public stEngineKeys As String()
    Public btDukptKey() As Byte

    ' variables de autenticacion
    Public btServerRandom() As Byte = {&H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0}
    Public btServerToken() As Byte = {&H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0}
    Public btSessionKey As Byte()
    Public stComandos(4) As String
    Public stSerialID As String
    Public btJackSerial(16) As Byte
    Public inPosComando As Integer = 0
    Public bBanderaComando As Boolean = False
    Public inContadorComandos As Integer = 3
    Public inCantRegistros As Integer = 0
    Public inCantAid As Integer = 0
    Public inCantAidAux As Integer = 0
    Public inCantCapk As Integer = 0
    Public inCantCapkAux As Integer = 0
    Public strPath As String = "configuration.xml"
    Public blTipoProceso As Boolean = False

End Module
