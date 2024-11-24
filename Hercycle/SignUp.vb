﻿Imports MySql.Data.MySqlClient
Public Class SignUp


    Private Sub btn_signup_Click_1(sender As Object, e As EventArgs) Handles btn_signup.Click
        Dim validInput As Boolean = False

        Do While Not validInput
            ' Check if the passwords match
            If txt_password.Text <> txt_cnfirmpassword.Text Then
                MessageBox.Show("Passwords do not match. Please try again.", "Password Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub ' Exit if the user cancels or needs to fix the input
            End If

            ' Check if the password is strong enough
            If txt_password.Text.Length < 8 Then
                MessageBox.Show("Password must be at least 8 characters long.", "Weak Password", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            ' Validate other inputs (e.g., empty fields)
            If String.IsNullOrWhiteSpace(txt_givenname.Text) OrElse
           String.IsNullOrWhiteSpace(txt_surname.Text) OrElse
           String.IsNullOrWhiteSpace(txt_email.Text) Then
                MessageBox.Show("Please fill in all required fields.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            ' If all inputs are valid, proceed with database insertion
            Try
                Dim dbconnect As New dbconnect
                dbconnect.connect()

                Dim query As String = "INSERT INTO tbl_user (firstname, middleinitial, lastname, email, age, birthdate, username, password) VALUES (@firstname, @middleinitial, @lastname, @email, @age, @birthdate, @username, @password)"
                Dim cmd As New MySqlCommand(query, dbconnect.conn)
                cmd.Parameters.AddWithValue("@firstname", txt_givenname.Text)
                cmd.Parameters.AddWithValue("@middleinitial", txt_middleint.Text)
                cmd.Parameters.AddWithValue("@lastname", txt_surname.Text)
                cmd.Parameters.AddWithValue("@email", txt_email.Text)
                cmd.Parameters.AddWithValue("@age", txt_age.Text)
                cmd.Parameters.AddWithValue("@birthdate", datetmpick_birthdate.Text)

                cmd.Parameters.AddWithValue("@username", txt_username.Text)
                cmd.Parameters.AddWithValue("@password", txt_password.Text)
                cmd.ExecuteNonQuery()

                MessageBox.Show("User successfully registered.", "Registration Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
                validInput = True ' Exit the loop
                LogIn.Show()
                Me.Close()

            Catch ex As Exception
                MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Loop
    End Sub


    Private Sub linklbl_login_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles linklbl_login.LinkClicked
        LogIn.Show()
        Me.Hide()
    End Sub
End Class