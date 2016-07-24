﻿Imports System.Runtime.CompilerServices

Public Module modHelpers
    Const ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
    Const LENGTH_OF_ALPHABET = 26

    'hacky method to replace a char in a string
    <Extension()>
    Function replacedAtIndex(someString As String, index As Integer, newLetter As Char) As String
        Return someString.Remove(index, 1).Insert(index, newLetter)
    End Function

    ' ENCODING FUNCTIONS:
    ' (Note: converts text to uppercase as well)
    Function encodeCaesar(message As String, rot As Integer) As String
        message = message.ToUpper()

        For i = 0 To message.Length - 1
            Dim letter As Char = message(i)
            If Char.IsLetter(letter) Then
                message = message.replacedAtIndex(i, shiftLetter(letter, rot)) 'replace each letter with it's shifted version
            End If
        Next

        Return message
    End Function

    Function encodeVigenere(message As String, key As String) As String
        message = message.ToUpper()
        key = key.ToUpper()

        Dim currentKeyIndex = 0

        For i = 0 To message.Length - 1
            Dim letter As Char = message(i)

            If Char.IsLetter(letter) Then
                Dim currentKeyChar = key(currentKeyIndex Mod key.Length)
                Dim rot As Integer = ALPHABET.IndexOf(currentKeyChar)

                message = message.replacedAtIndex(i, shiftLetter(letter, rot))
                currentKeyIndex += 1
            End If
        Next

        Return message
    End Function

    ' HELPER FUNCTIONS:
    Function shiftLetter(letter As Char, rot As Integer) As Char
        rot = rot Mod 26

        Dim newLetter As Char = ALPHABET((ALPHABET.IndexOf(letter) + rot) Mod LENGTH_OF_ALPHABET)

        Return newLetter
    End Function

    ' UNIT TESTS:
    Sub testCaesar()
        Debug.Print("Testing Caesar Encoding!")
        Debug.Assert(encodeCaesar("abc", 0) = "ABC")
        Debug.Assert(encodeCaesar("abc", 1) = "BCD")
        Debug.Assert(encodeCaesar("abc", 2) = "CDE")
        Debug.Assert(encodeCaesar("abc", 25) = "ZAB")
        Debug.Assert(encodeCaesar("abc", 26) = "ABC")
        Debug.Assert(encodeCaesar("abc", 27) = "BCD")
        Debug.Print("All tests Passed!!!!!")
    End Sub

    Sub testVigenere()
        Debug.Print("Testing Vigenere Encoding!")
        Debug.Assert(encodeVigenere("aaaaaaa", "abc") = "ABCABCA")
        Debug.Assert(encodeVigenere("aaaaaaa", "bcd") = "BCDBCDB")
        Debug.Assert(encodeVigenere("asdfghjkl", "xyz") = "XQCCEGGIK")
        Debug.Assert(encodeVigenere("HI THERE IS THIS KIND OF COOL???", "xyz") = "EG SECQB GR QFHP IHKB NC ANLJ???")
        Debug.Assert(encodeVigenere("!@#$%^&*(", "abc") = "!@#$%^&*(")
        Debug.Print("All tests Passed!!!!!")
    End Sub

    ' POSITIONING HELPERS:
    <Extension()>
    Sub horizontallyCentre(control As Object)
        control.Left = formMain.Width / 2 - control.Width / 2
    End Sub

    <Extension()>
    Sub verticallyCentre(control As Object)
        control.Top = formMain.Height / 2 - control.Height / 2
    End Sub

    <Extension()>
    Sub placeAbove(control As Object, relativeControl As Object, pixels As Integer)
        control.Top = relativeControl.Top - control.Height - pixels
    End Sub

    <Extension()>
    Sub placeBelow(control As Object, relativeControl As Object, pixels As Integer)
        control.Top = relativeControl.Top + relativeControl.Height + pixels
    End Sub

    <Extension()>
    Sub placeRight(control As Object, relativeControl As Object, pixels As Integer)
        control.Left = relativeControl.Left + relativeControl.Width + pixels
    End Sub

    <Extension()>
    Sub placeLeft(control As Object, relativeControl As Object, pixels As Integer)
        control.Left = relativeControl.Left - relativeControl.Width - pixels
    End Sub

    ' BORDER DRAWING

    <Extension()>
    Sub drawBorder(control As Control, color As Color)
        Dim g As Graphics = control.FindForm().CreateGraphics()
        Dim pen As New Pen(color, 4)

        'make sure control argument is a textbox
        'If TypeOf (control) Is TextBox Then
        g.DrawRectangle(pen, New Rectangle(control.Location, control.Size))
        'End If

        pen.Dispose()
        g.Dispose()
    End Sub

    ' This is used inside the onPaint event. Not really reusable idk why.
    <Extension()>
    Sub drawBottomBorder(control As Control, g As Graphics, color As Color)
        Dim pen As New Pen(color, 4)
        g.DrawLine(pen, New Point(control.Left, control.Top + control.Height), New Point(control.Left + control.Width, control.Top + control.Height))

        pen.Dispose()
    End Sub

    ' CHANGING A CHARACTER IN A STRNIG
    Function changeCharacter(s As String, replaceWith As Char, index As Integer) As String
        Dim sb As New System.Text.StringBuilder(s)

        'replace character
        sb(index) = replaceWith
        Return sb.ToString()
    End Function
End Module