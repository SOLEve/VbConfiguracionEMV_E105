Imports System.Data.SqlClient

Public Class FirmaDB


#Region "Declaración de variable de BD"
    Private sqlConn As SqlConnection = New SqlConnection
    Private sqlComm As SqlCommand = New SqlCommand
    Private sqlData As SqlDataAdapter = New SqlDataAdapter
    Private sqlDatr As SqlDataReader
    Private sqlDats As DataSet = New DataSet
#End Region


#Region "Funciones de BD"

    Public Function inSetDBtoConnect(ByVal strServer As String, ByVal strPwd As String) As Integer
        sqlConn.ConnectionString = "Server=" + strServer + ";Database=TESTFIRMADB;User=sa;pwd=" + strPwd + ";Trusted_Connection=False"
        'sqlConn.ConnectionString = "Server=(local);Database=TESTFIRMADB;User=sa;pwd=;Trusted_Connection=False"
        'sqlConn.ConnectionString = "Server=DESARROLLO3\SQLEXPRESS;Database=TESTFIRMADB;User=sa;pwd=;Trusted_Connection=False"
    End Function

    Public Function inObtenernNombreTransaccion(ByRef stNombreTrans As String, ByVal stTransid As String) As Integer
        sqlConn.Open()
        Dim prm1 As SqlParameter = sqlComm.Parameters.Add("@SQLString", SqlDbType.NVarChar, 500)

        prm1.Value = "SELECT TRANSACCION FROM tblTransacciones WHERE ID='" & stTransid & "'"

        sqlComm.Connection = sqlConn
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.CommandText = "sp_executesql"

        Try
            sqlDatr = sqlComm.ExecuteReader()

            If sqlDatr.Read() Then
                stNombreTrans = CStr(sqlDatr.Item(0))
                sqlConn.Close()
                Return 1 '
            Else
                sqlConn.Close()
                Return 2 'Variable NO Encontrada
            End If
        Catch ex As System.Data.SqlClient.SqlException
            sqlConn.Close()
            sqlDats.Clear()
            Return -1 ' DB Error
        End Try
    End Function

    Public Function inObtenerListaTransacciones(ByRef dsListaTransacciones As DataSet) As Integer
        sqlConn.Open()
        Dim prm1 As SqlParameter = sqlComm.Parameters.Add("@SQLString", SqlDbType.NVarChar, 4000)

        prm1.Value = "SELECT ID, TRANSACCION, REQUEST, REPLY, ACTIVA, RECIBE FROM tblTransacciones"

        sqlComm.Connection = sqlConn
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.CommandText = "sp_executesql"

        Try
            sqlDatr = sqlComm.ExecuteReader()

            If Not sqlDatr.Read() Then
                Return 2 ' No existen Transacciones
            Else
                sqlDatr.Close()
                sqlData.SelectCommand = sqlComm
                sqlData.Fill(dsListaTransacciones)
                sqlConn.Close()
                sqlDats.Clear()
                Return 1 ' Si existen Transacciones
            End If
        Catch ex As Exception
            sqlConn.Close()
            sqlDats.Clear()
            Return -1 ' DB Error
        End Try
    End Function

    Public Function inObtenerListaTransacciones(ByRef dsListaTransacciones As DataSet, ByVal inId As Integer) As Integer
        sqlConn.Open()
        Dim prm1 As SqlParameter = sqlComm.Parameters.Add("@SQLString", SqlDbType.NVarChar, 4000)

        prm1.Value = ""
        prm1.Value = "SELECT TRANSACCION, REQUEST, REPLY, ACTIVA, RECIBE FROM tblTransacciones WHERE ID =" & inId

        sqlComm.Connection = sqlConn
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.CommandText = "sp_executesql"

        Try
            sqlDatr = sqlComm.ExecuteReader()

            If Not sqlDatr.Read() Then
                Return 2 ' No existen Transacciones
            Else
                sqlDatr.Close()
                sqlData.SelectCommand = sqlComm
                sqlData.Fill(dsListaTransacciones)
                sqlConn.Close()
                sqlDats.Clear()
                Return 1 ' Si existen Transacciones
            End If
        Catch ex As Exception
            sqlConn.Close()
            sqlDats.Clear()
            Return -1 ' DB Error
        End Try
    End Function

    Public Function inObtenerTransaccionesPorRequest(ByRef dsListaTransacciones As DataSet, ByVal inRequest As String) As Integer
        sqlConn.Open()
        Dim prm1 As SqlParameter = sqlComm.Parameters.Add("@SQLString", SqlDbType.NVarChar, 4000)

        prm1.Value = "SELECT TRANSACCION, REPLY, ID, RECIBE FROM tblTransacciones " & _
                    " WHERE REQUEST ='" & inRequest & "' AND ACTIVA='S'"

        sqlComm.Connection = sqlConn
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.CommandText = "sp_executesql"

        Try
            sqlDatr = sqlComm.ExecuteReader()

            If Not sqlDatr.Read() Then
                sqlDatr.Close()
                sqlConn.Close()
                sqlDats.Clear()
                sqlComm.Parameters.Clear()
                Return 2 ' No existen Transacciones
            Else
                sqlDatr.Close()
                sqlData.SelectCommand = sqlComm
                sqlData.Fill(dsListaTransacciones)
                sqlConn.Close()
                sqlDats.Clear()
                sqlComm.Parameters.Clear()
                Return 1 ' Si existen Transacciones
            End If
        Catch ex As Exception
            sqlDatr.Close()
            sqlConn.Close()
            sqlDats.Clear()
            sqlComm.Parameters.Clear()
            Return -1 ' DB Error
        End Try
    End Function

    Public Function CrearNuevaTransaccion(ByVal stNombre As String, ByVal stRequest As String, ByVal stReply As String, ByVal stActiva As String, ByVal stRecibe As String) As Integer
        sqlConn.Open()
        Dim prm1 As SqlParameter = sqlComm.Parameters.Add("@SQLString", SqlDbType.NVarChar, 4000)
        prm1.Value = "INSERT tblTransacciones " & _
        " (TRANSACCION,REQUEST,REPLY,ACTIVA,RECIBE) VALUES " & _
        "('" & stNombre & "','" & stRequest & "','" & stReply & "','" & stActiva & "','" & stRecibe & "')"

        sqlComm.Connection = sqlConn
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.CommandText = "sp_executesql"

        Try
            sqlComm.ExecuteNonQuery()
            sqlConn.Close()
            sqlDats.Clear()
            Return 2 ' Insert OK
        Catch ex As Exception
            sqlConn.Close()
            sqlDats.Clear()
            Return -1 ' Insert Error
        End Try
    End Function

    Public Function inActualizarTransaccion(ByVal stTransaccion As String, ByVal stRequest As String, ByVal stReply As String, ByVal inID As Integer, ByVal stActiva As String, ByVal stRecibe As String) As Integer
        sqlConn.Open()
        Dim prm1 As SqlParameter = sqlComm.Parameters.Add("@SQLString", SqlDbType.NVarChar, 4000)
        'Dim dr As DataRow
        Dim inFila As Integer = 0

        prm1.Value = "UPDATE tblTransacciones SET " & _
        " TRANSACCION='" & stTransaccion & "', REQUEST='" & stRequest & "', REPLY='" & stReply & "', ACTIVA='" & stActiva & "', RECIBE='" & stRecibe & "'" & _
        " WHERE ID=" & inID

        sqlComm.Connection = sqlConn
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.CommandText = "sp_executesql"

        Try
            sqlComm.ExecuteNonQuery()
            sqlConn.Close()
            sqlDats.Clear()
            Return 2 ' Actualizada Informacion 
        Catch ex As System.Data.SqlClient.SqlException
            sqlConn.Close()
            sqlDats.Clear()
            Return -1  ' No se Actualizo Informacion 
        End Try
    End Function

    Public Function inEliminarTransaccion(ByVal inID As Integer) As Integer
        sqlConn.Open()
        Dim prm1 As SqlParameter = sqlComm.Parameters.Add("@SQLString", SqlDbType.NVarChar, 4000)

        prm1.Value = "DELETE FROM tblTransacciones WHERE ID=" & inID

        sqlComm.Connection = sqlConn
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.CommandText = "sp_executesql"

        Try
            sqlComm.ExecuteNonQuery()
            sqlConn.Close()
            sqlDats.Clear()
            Return 2
        Catch ex As Exception
            sqlConn.Close()
            sqlDats.Clear()
            Return -1 ' DB Error
        End Try
    End Function

    Public Function inObetenerActiva(ByRef stIsActiva As String, ByVal inID As Integer) As Integer
        Try
            sqlConn.Open()
        Catch ex As System.Data.SqlClient.SqlException
            sqlConn.Close()
            sqlDats.Clear()
            Return -1 ' DB Error
        End Try

        Dim prm1 As SqlParameter = sqlComm.Parameters.Add("@SQLString", SqlDbType.NVarChar, 800)

        prm1.Value = "SELECT ACTIVA FROM tblTransacciones WHERE ID ='" & inID & "'"

        sqlComm.Connection = sqlConn
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.CommandText = "sp_executesql"

        Try
            sqlDatr = sqlComm.ExecuteReader()

            If sqlDatr.Read() Then
                stIsActiva = CStr(sqlDatr.Item(0))
                sqlDatr.Close()
                sqlConn.Close()
                Return 1 ' Found
            Else
                sqlDatr.Close()
                sqlConn.Close()
                Return 2 ' Not found
            End If
        Catch ex As System.Data.SqlClient.SqlException
            sqlDatr.Close()
            sqlConn.Close()
            Return -1 ' DB Error
        End Try
    End Function

    Public Function inObetenerRecibe(ByRef stRecibe As String, ByVal inID As Integer) As Integer
        Try
            sqlConn.Open()
        Catch ex As System.Data.SqlClient.SqlException
            sqlConn.Close()
            sqlDats.Clear()
            Return -1 ' DB Error
        End Try

        Dim prm1 As SqlParameter = sqlComm.Parameters.Add("@SQLString", SqlDbType.NVarChar, 800)

        prm1.Value = "SELECT RECIBE FROM tblTransacciones WHERE ID ='" & inID & "'"

        sqlComm.Connection = sqlConn
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.CommandText = "sp_executesql"

        Try
            sqlDatr = sqlComm.ExecuteReader()

            If sqlDatr.Read() Then
                stRecibe = CStr(sqlDatr.Item(0))
                sqlDatr.Close()
                sqlConn.Close()
                Return 1 ' Found
            Else
                sqlDatr.Close()
                sqlConn.Close()
                Return 2 ' Not found
            End If
        Catch ex As System.Data.SqlClient.SqlException
            sqlDatr.Close()
            sqlConn.Close()
            Return -1 ' DB Error
        End Try
    End Function

    Function TestDBConnection() As Boolean
        Try
            sqlConn.Open()
        Catch ex As System.Data.SqlClient.SqlException
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "INFORMACIÓN DEL SISTEMA")
            sqlConn.Close()
            sqlDats.Clear()
            Return False
        End Try

        sqlConn.Close()
        Return True
    End Function
#End Region
End Class
