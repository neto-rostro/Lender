'€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€
' Guanzon Software Engineering Group
' Guanzon Group of Companies
' Perez Blvd., Dagupan City
'
'     LR Application Entry Form
'
' Copyright 2016 and Beyond
' All Rights Reserved
' ºººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººº
' €  All  rights reserved. No part of this  software  €€  This Software is Owned by        €
' €  may be reproduced or transmitted in any form or  €€                                   €
' €  by   any   means,  electronic   or  mechanical,  €€    GUANZON MERCHANDISING CORP.    €
' €  including recording, or by information  storage  €€     Guanzon Bldg. Perez Blvd.     €
' €  and  retrieval  systems, without  prior written  €€           Dagupan City            €
' €  from the author.                                 €€  Tel No. 522-1085 ; 522-9275      €
' ºººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººººº
'
' ==========================================================================================
'  Kalyptus [ 07/12/2016 01:13 pm ]
'      Started creating of this object.
'€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€
Imports ggcLRTransaction.LRApplication
Imports ggcAppDriver

Public Class frmLRApplicationEntry
    Private WithEvents poTrans As ggcLRTransaction.LRApplication
    Private pnLoadx As Integer
    Private poControl As Control

    Private Sub frmLRApplicationEntry_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Debug.Print("frmLRApplicationEntry_Activated")
        If pnLoadx = 1 Then
            If poTrans.NewTransaction() Then
                Call loadMaster(Me.Panel1)
                Call showEditMode(Me, xeEditMode.MODE_ADDNEW)
            End If

            txtSeeks00.Text = poTrans.BranchName
            If p_oAppDriver.ProductID <> "LRTrackr" Then
                txtSeeks00.ReadOnly = True
                txtSeeks00.TabStop = False
                txtField01.Focus()
            Else
                txtSeeks00.ReadOnly = False
                txtSeeks00.TabStop = True
                txtSeeks00.Focus()
            End If

            pnLoadx = 2

        End If
    End Sub

    Private Sub frmLRApplicationEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F3 Or e.KeyCode = Keys.Enter Then
            Dim loTxt As Control

            loTxt = Nothing
            If TypeOf poControl Is TextBox Then
                loTxt = CType(poControl, System.Windows.Forms.TextBox)
            ElseIf TypeOf poControl Is CheckBox Then
                loTxt = CType(poControl, System.Windows.Forms.CheckBox)
            ElseIf TypeOf poControl Is ComboBox Then
                loTxt = CType(poControl, System.Windows.Forms.ComboBox)
            End If

            '*********************
            Dim loIndex As Integer
            loIndex = Val(Mid(loTxt.Name, 9))

            If Mid(loTxt.Name, 1, 8) = "txtSeeks" Then
                Select Case loIndex
                    Case 0
                        Call poTrans.searchBranch(loTxt.Text, False, True)
                        txtSeeks00.Text = poTrans.BranchName
                End Select
            ElseIf Mid(loTxt.Name, 1, 8) = "txtField" Then
                Select Case loIndex
                    Case 80
                        Call poTrans.SearchMaster(loIndex, loTxt.Text)
                End Select
            End If

            '*********************
            If TypeOf poControl Is TextBox Then
                SelectNextControl(loTxt, True, True, True, True)
            End If
        End If
    End Sub

    Private Sub frmLRApplicationEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Debug.Print("frmLRApplicationEntry_Load")
        If pnLoadx = 0 Then
            poTrans = New ggcLRTransaction.LRApplication(p_oAppDriver)

            'Set event Handlers for txtField
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
            Call grpCancelHandler(Me, GetType(TextBox), "txtField", "Validating", AddressOf txtField_Validating)

            Call grpEventHandler(Me, GetType(TextBox), "txtSeeks", "GotFocus", AddressOf txtSeeks_GotFocus)
            Call grpEventHandler(Me, GetType(TextBox), "txtSeeks", "LostFocus", AddressOf txtSeeks_LostFocus)

            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)

            pnLoadx = 1
        End If
    End Sub

    Private Sub loadMaster(ByVal loControl As Control)
        Dim loTxt As Control

        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadMaster(loTxt)
            Else
                If (TypeOf loTxt Is TextBox) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loTxt.Name, 9))
                    If LCase(Mid(loTxt.Name, 1, 8)) = "txtfield" Then
                        Select Case loIndex
                            Case 1, 7
                                If IsDate(poTrans.Master(loIndex)) Then
                                    loTxt.Text = Format(poTrans.Master(loIndex), "MMMM dd, yyyy")
                                Else
                                    loTxt.Text = ""
                                End If
                            Case 5, 6
                                If IsNumeric(poTrans.Master(loIndex)) Then
                                    loTxt.Text = Format(poTrans.Master(loIndex), xsDECIMAL)
                                Else
                                    loTxt.Text = "0.00"
                                End If
                            Case Else
                                loTxt.Text = IIf(poTrans.EditMode = xeEditMode.MODE_UNKNOWN, "", poTrans.Master(loIndex))
                        End Select
                    End If 'LCase(Mid(loTxt.Name, 1, 8)) = "txtfield"
                End If '(TypeOf loTxt Is TextBox)
            End If 'If loTxt.HasChildren
        Next 'loTxt In loControl.Controls
    End Sub

    Private Sub showEditMode(ByVal loControl As Control, ByVal fnEditMode As xeEditMode)
        Dim loBtn As Control

        For Each loBtn In loControl.Controls
            If loBtn.HasChildren Then
                Call showEditMode(loBtn, fnEditMode)
            Else
                If (TypeOf loBtn Is Button) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loBtn.Name, 10))
                    If LCase(Mid(loBtn.Name, 1, 9)) = "cmdbutton" Then
                        Select Case fnEditMode
                            Case xeEditMode.MODE_ADDNEW, xeEditMode.MODE_UPDATE
                                loBtn.Visible = IIf(loIndex = 0 Or loIndex = 1, False, True)
                            Case Else
                                loBtn.Visible = IIf(loIndex = 0 Or loIndex = 1, True, False)
                        End Select
                    End If 'LCase(Mid(loTxt.Name, 1, 9)) = "cmdButton"
                ElseIf (TypeOf loBtn Is TextBox) Then
                    Dim loIndex As Integer
                    loIndex = Val(Mid(loBtn.Name, 9))
                    If loBtn.TabStop Then
                        Select Case fnEditMode
                            Case xeEditMode.MODE_ADDNEW, xeEditMode.MODE_UPDATE
                                loBtn.Enabled = IIf(LCase(Mid(loBtn.Name, 1, 8)) = "txtfield", True, False)
                            Case Else
                                loBtn.Enabled = IIf(LCase(Mid(loBtn.Name, 1, 8)) = "txtfield", False, True)
                        End Select
                    End If
                End If '(TypeOf loTxt Is Button)
            End If 'If loTxt.HasChildren
        Next 'loTxt In loControl.Controls
    End Sub

    'Handles GotFocus Events for txtField & txtField
    Private Sub txtField_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        poControl = loTxt

        loTxt.BackColor = Color.Azure
        loTxt.SelectAll()
    End Sub

    'Handles LostFocus Events for txtField & txtField
    Private Sub txtField_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        loTxt.BackColor = SystemColors.Window
    End Sub

    'Handles Validating Events for txtField & txtField
    Private Sub txtField_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)

        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))
        If Mid(loTxt.Name, 1, 8) = "txtField" And loTxt.ReadOnly = False Then
            poTrans.Master(loIndex) = loTxt.Text
        End If
    End Sub

    Private Sub cmdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim loChk As Button
        loChk = CType(sender, System.Windows.Forms.Button)

        Dim lnIndex As Integer
        lnIndex = Val(Mid(loChk.Name, 10))

        Select Case lnIndex
            Case 0 ' Exit
                Me.Dispose()
            Case 1 ' New
                If poTrans.NewTransaction Then
                    Call loadMaster(Me.Panel1)
                    Call showEditMode(Me, xeEditMode.MODE_ADDNEW)
                    txtField01.Focus()
                End If
            Case 2 ' Search
                Dim loIndex As Integer
                loIndex = Val(Mid(poControl.Name, 10))

                If loIndex = 80 Then
                    poTrans.SearchMaster(loIndex, poControl.Text)
                ElseIf loIndex = 0 Then
                    poTrans.SearchBranch(poControl.Text, False, True)
                End If
            Case 3 ' Cancel Update
                If MsgBox("Do you really want to discard all changes?", MsgBoxStyle.Information, "LR Application Entry") Then
                    poTrans.NewTransaction()
                    Call loadMaster(Me.Panel1)
                    showEditMode(Me, xeEditMode.MODE_READY)
                    txtSeeks00.Focus()
                End If
            Case 4 'Save confirmation
                If poTrans.SaveTransaction Then
                    MsgBox("LR Application was save successfully!", MsgBoxStyle.Information, "LR Application Entry")
                    showEditMode(Me, xeEditMode.MODE_READY)
                    txtSeeks00.Focus()
                Else
                    MsgBox("Unable to save LR Application!", MsgBoxStyle.Information, "LR Application Entry")
                    txtField01.Focus()
                End If
        End Select
    End Sub

    Private Sub txtSeeks_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        poControl = loTxt
        loTxt.BackColor = Color.Azure
        loTxt.SelectAll()
    End Sub

    Private Sub txtSeeks_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        loTxt.BackColor = SystemColors.Window
        poControl = Nothing
    End Sub

    Private Sub poTrans_MasterRetrieved(Index As Integer, Value As Object) Handles poTrans.MasterRetrieved
        Dim loTxt As TextBox
        'Find TextBox with specified name
        loTxt = CType(FindTextBox(Me, "txtField" & Format(Index, "00")), TextBox)

        Select Case Index
            Case 80
                loTxt.Text = Value
                txtField81.Text = poTrans.Master(81)
            Case 5, 6
                loTxt.Text = Format(Value, xsDECIMAL)
            Case 1, 7
                loTxt.Text = Format(Value, xsDATE_MEDIUM)
            Case Else
                loTxt.Text = Value
        End Select
    End Sub
End Class