Imports System
Imports System.Data
Module Program
    Sub Main(args As String())
        Dim conectionString As String = "Server=localhost;Port=5470;User Id=postgres;Password=postgres;Database=Punto1"
        Dim connection = New Npgsql.NpgsqlConnection(conectionString)
        connection.Open()
        Dim dt = levantarTabla(connection, "SELECT * FROM Schema1.mediosunificadab")
        For Each row As DataRow In dt.Rows
            row("medio") = row("medio").ToString().Replace("'", " ")
            row("especialidad") = row("especialidad").ToString().Replace("'", " ")
            row("tipo_de_medio") = row("tipo_de_medio").ToString().Replace("'", " ")
            row("direccion") = row("direccion").ToString().Replace("'", " ")
            row("ciudad") = row("ciudad").ToString().Replace("'", " ")
            row("provincia") = row("provincia").ToString().Replace("'", " ")
            Dim mediosQuery As String = "insert into Schema1.medios (id, medio, id_especialidad, " &
                "id_tipo_medio, direccion,id_ciudad) values ('" &
                row("id_de_medio") & "','" &
                row("medio") & "','" &
                row("especialidad") & "','" &
                row("tipo_de_medio") & "','" &
                row("direccion") & "','" &
                row("ciudad") & "')"
            Dim c0 As New Npgsql.NpgsqlCommand(mediosQuery, connection)
            c0.ExecuteNonQuery()
            Try
                Dim especialidadInsert As String = "insert into Schema1.especialidad (id, descripcion) values (" &
                    "'" & row("especialidad") & "','" & row("especialidad") & "')"
                Dim c1 As New Npgsql.NpgsqlCommand(especialidadInsert, connection)
                c1.ExecuteNonQuery()
            Catch ex As Exception
                'PK exists
            End Try
            Try
                Dim tipoMediosInsert As String = "insert into Schema1.tipo_medio (id, descripcion) values (" &
                    "'" & row("tipo_de_medio") & "','" & row("tipo_de_medio") & "')"
                Dim c2 As New Npgsql.NpgsqlCommand(tipoMediosInsert, connection)
                c2.ExecuteNonQuery()
            Catch ex As Exception
                'PK exists
            End Try
            Try
                Dim ciudadInsert As String = "insert into Schema1.ciudad (id, nombre, id_provincia) values (" &
                    "'" & row("ciudad") & "','" & row("ciudad") & "','" & row("provincia") & "')"
                Dim c3 As New Npgsql.NpgsqlCommand(ciudadInsert, connection)
                c3.ExecuteNonQuery()
            Catch ex As Exception
                'PK exists
            End Try
            Try
                Dim provinciaInsert As String = "insert into Schema1.provincia (id, nombre) values (" &
                    "'" & row("provincia") & "','" & row("provincia") & "')"
                Dim c4 As New Npgsql.NpgsqlCommand(provinciaInsert, connection)
                c4.ExecuteNonQuery()
            Catch ex As Exception
                'PK 
            End Try
            Console.WriteLine("Procesada fila: " & row("id_de_medio"))
        Next
        Console.WriteLine("Finalizado")
        connection.Close()
    End Sub
    Private Function levantarTabla(connection As Npgsql.NpgsqlConnection, query As String) As DataTable
        Dim dt As New DataTable
        Dim c As New Npgsql.NpgsqlCommand(query, connection)
        Dim a As New Npgsql.NpgsqlDataAdapter(c)
        a.Fill(dt)
        Return dt
    End Function
End Module
