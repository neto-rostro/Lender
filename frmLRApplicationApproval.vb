'€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€
' Guanzon Software Engineering Group
' Guanzon Group of Companies
' Perez Blvd., Dagupan City
'
'     LR Application Approval Form
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
'  Kalyptus [ 07/12/2016 04:57 pm ]
'      Started creating of this object.
'€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€€
Imports ggcLRTransaction.LRApplication
Imports ggcAppDriver

Public Class frmLRApplicationApproval
    Private WithEvents poTrans As ggcLRTransaction.LRApplication
    Private pnLoadx As Integer
    Private poControl As Control

    Private Sub frmLRApplicationApproval_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Debug.Print("frmLRApplicationApproval_Activated")
        If pnLoadx = 1 Then
            Call loadMaster(Me.Panel1)
            Call loadMaster(Me.Panel2)
            txtSeeks01.Text = IIf(poTrans.Master("sClientNm") = "0", "", poTrans.Master("sClientNm"))
            txtSeeks02.Text = IIf(poTrans.Master("sTransNox") = "0", "", poTrans.Master("sTransNox"))

            txtSeeks00.Text = poTrans.BranchName
            If p_oAppDriver.ProductID <> "LRTrackr" Then
                txtSeeks00.ReadOnly = True
                txtSeeks00.TabStop = False
                txtSeeks01.Focus()
            Else
                txtSeeks00.ReadOnly = False
                txtSeeks00.TabStop = True
                txtSeeks00.Focus()
            End If

            pnLoadx = 2

        End If
    End Sub

    Private Sub frmLRApplicationApproval_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
                    Case 1
                        Call poTrans.SearchTransaction(loTxt.Text, IIf(loIndex = 1, False, True))
                        Call loadMaster(Me.Panel1)
                        Call loadMaster(Me.Panel2)
                        txtSeeks01.Text = IIf(poTrans.Master("sClientNm") = "0", "", poTrans.Master("sClientNm"))
                        txtSeeks02.Text = IIf(poTrans.Master("sTransNox") = "0", "", poTrans.Master("sTransNox"))
                End Select
            End If

            '*********************
            If TypeOf poControl Is TextBox Then
                SelectNextControl(loTxt, True, True, True, True)
            End If
        End If
    End Sub

    Private Sub frmLRApplicationApproval_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Debug.Print("frmLRApplicationApproval_Load")
        If pnLoadx = 0 Then
            poTrans = New ggcLRTransaction.LRApplication(p_oAppDriver, 0)

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
                                If IsNumeric(poTrans.Master(loIndex)) And poTrans.Master(loIndex) <> vbEmpty Then
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

        If loTxt.ReadOnly Then
            loTxt.BackColor = SystemColors.Control
        Else
            loTxt.BackColor = SystemColors.Window
        End If
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
            Case 1 ' Disapproved
                If poTrans.CancelTransaction Then
                    poTrans.NewTransaction()
                    Call loadMaster(Me.Panel1)
                    Call loadMaster(Me.Panel2)
                    txtSeeks01.Text = IIf(poTrans.Master("sClientNm") = "0", "", poTrans.Master("sClientNm"))
                    txtSeeks02.Text = IIf(poTrans.Master("sTransNox") = "0", "", poTrans.Master("sTransNox"))

                    MsgBox("Application was cancelled sucessfully...", , "Application Approval")
                Else
                    MsgBox("Unable to cancel application...", , "Application Approval")
                End If
            Case 2 ' Approved
                If poTrans.PostTransaction Then

                    poTrans.NewTransaction()
                    Call loadMaster(Me.Panel1)
                    Call loadMaster(Me.Panel2)
                    txtSeeks01.Text = IIf(poTrans.Master("sClientNm") = "0", "", poTrans.Master("sClientNm"))
                    txtSeeks02.Text = IIf(poTrans.Master("sTransNox") = "0", "", poTrans.Master("sTransNox"))

                    MsgBox("Application was approved sucessfully...", , "Application Approval")
                Else
                    MsgBox("Unable to approve application...", , "Application Approval")
                End If
            Case 3 ' Search
                Dim loIndex As Integer
                loIndex = Val(Mid(poControl.Name, 10))

                If loIndex = 0 Then
                    poTrans.SearchBranch(poControl.Text, False, True)
                    txtSeeks00.Text = poTrans.BranchName
                ElseIf loIndex = 1 Or loIndex = 2 Then
                    If poTrans.SearchTransaction(poControl.Text, IIf(loIndex = 1, False, True)) Then
                        Call loadMaster(Me.Panel1)
                        Call loadMaster(Me.Panel2)
                        txtSeeks01.Text = IIf(poTrans.Master("sClientNm") = "0", "", poTrans.Master("sClientNm"))
                        txtSeeks02.Text = IIf(poTrans.Master("sTransNox") = "0", "", poTrans.Master("sTransNox"))
                    End If
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
        If loTxt.ReadOnly Then
            loTxt.BackColor = SystemColors.Control
        Else
            loTxt.BackColor = SystemColors.Window
        End If
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