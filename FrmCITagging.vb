Imports ggcAppDriver
Imports ggcGOCAS
Imports ggcGOCAS.GOCASCI
Imports Newtonsoft.Json

Public Class FrmCITagging
    Public poTrans As GOCASCI

    Private p_oResidence As residence_info
    Private p_oPropertyx As properties_info
    Private p_oMeansInfo As means_info

    Private p_xResidence As residence_info
    Private p_xPropertyx As properties_info
    Private p_xMeansInfo As means_info

    Dim lnMsg As String
    Dim poControl As Control
    Dim pnLoadx As Integer
    Dim pnStat As Integer
    Private p_sTransNox As String
    Private pxeModuleName As String = "Evaluator CI Tagging"

    Public WriteOnly Property sTransNox As String
        Set(ByVal value As String)
            p_sTransNox = value
        End Set
    End Property
    Private Sub FrmCITagging_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

        If pnLoadx = 1 Then
            Call clearFields()
            poTrans = New GOCASCI(p_oAppDriver)
            Call ctrlFields()


            poTrans.TransNo = p_sTransNox
            With poTrans
                pnLoadx = 2
                If poTrans.isRecordExist() = 0 Then
                    If poTrans.NewRecord() Then
                        Call ctrlFields()
                        'get field values
                        p_oResidence = poTrans.CI_Residence
                        p_oPropertyx = poTrans.CI_Property
                        p_oMeansInfo = poTrans.CI_Means_Info
                        'get result field values
                        p_xResidence = poTrans.Result_Residence
                        p_xPropertyx = poTrans.Result_Property
                        p_xMeansInfo = poTrans.Result_Means_Info

                        Call clearFields()
                        Call loadTransaction()
                    End If
                End If
            End With

            'pnLoadx = 2
        End If
    End Sub

    Private Sub cmdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim loChk As Button
        loChk = CType(sender, System.Windows.Forms.Button)

        Dim lnindex As Integer
        lnindex = Val(Mid(loChk.Name, 10))

        Select Case lnindex
            Case 0
                Me.Close()
            Case 1
                If MsgBox("Does all the information checked final?", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "Confirm") = MsgBoxResult.Ok Then
                    If poTrans.SaveRecord Then
                        MsgBox("CI Tagging Succesfully created!!!", vbInformation, "Information")
                        Me.Close()
                    End If
                End If
                   

        End Select
    End Sub
    Private Sub ArrowKeys_Keydown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Up, Keys.Down, Keys.Left, Keys.Right
                Select Case e.KeyCode
                    Case Keys.Down, Keys.Right
                        SetNextFocus()
                    Case Keys.Up, Keys.Left
                        SetPreviousFocus()
                End Select
        End Select
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
    Private Sub txtField_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        Dim loIndex As Integer
        loIndex = Val(Mid(loTxt.Name, 9))
        If Mid(loTxt.Name, 1, 8) = "txtField" And loTxt.ReadOnly = False Then

        End If
    End Sub
    Private Sub FrmCITagging_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If pnLoadx = 0 Then
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)

            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)

            Call grpCancelHandler(Me, GetType(TextBox), "txtField", "Validating", AddressOf txtField_Validating)

            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)

            Call grpEventHandler(Me, GetType(CheckBox), "chkBox", "CheckedChanged", AddressOf chkBox_CheckedChanged)

            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf txtField_KeyDown)
            pnLoadx = 1
        End If
    End Sub

    Private Sub chkBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim checkbox As CheckBox
        checkbox = CType(sender, System.Windows.Forms.CheckBox)
        Dim lnindex As Integer
        lnindex = Val(Mid(checkbox.Name, 7))
        Dim check() As CheckBox = {chkBox01, chkBox02, chkBox03, chkBox04, chkBox05, chkBox06, chkBox07, chkBox08, chkBox09, chkBox10,
                                  chkBox11, chkBox12, chkBox13, chkBox14, chkBox15, chkBox16, chkBox17, chkBox18, chkBox19, chkBox20,
                                  chkBox21, chkBox22, chkBox23, chkBox24, chkBox25, chkBox26, chkBox27, chkBox28, chkBox29, chkBox30, chkBox31, chkBox32, chkBox33}

        Dim x As Integer


        Select Case lnindex
            Case 1
                If chkBox01.Checked = True Then
                    For x = 1 To 2
                        check(x).Checked = True
                    Next
                Else
                    If chkBox02.Checked = True And chkBox03.Checked = True Then
                        For x = 1 To 2
                            If check(x).Checked = True Then
                                check(x).Checked = False
                            End If
                        Next
                    End If
                End If

            Case 2 To 3
                If (chkBox02.Checked = False) Or (chkBox03.Checked = False) Then
                    chkBox01.Checked = False
                ElseIf (chkBox02.Checked = True) And (chkBox03.Checked = True) Then
                    chkBox01.Checked = True
                End If

            Case 4
                If check(3).Checked = True Then
                    For x = 4 To 12
                        check(x).Checked = True
                    Next
                Else
                    If chkBox05.Checked = True And chkBox06.Checked = True And
                        chkBox07.Checked = True And chkBox08.Checked = True And
                        chkBox09.Checked = True And chkBox10.Checked = True And
                        chkBox11.Checked = True And chkBox12.Checked = True And chkBox13.Checked = True Then

                        For x = 4 To 12
                            If check(x).Checked = True Then
                                check(x).Checked = False
                            End If
                        Next
                    End If
                End If
            Case 5 To 13

                If chkBox05.Checked = False Or chkBox06.Checked = False Or
                   chkBox07.Checked = False Or chkBox08.Checked = False Or
                   chkBox09.Checked = False Or chkBox10.Checked = False Or
                   chkBox11.Checked = False Or chkBox12.Checked = False Or chkBox13.Checked = False Then

                    chkBox04.Checked = False
                Else
                    For x = 3 To 12
                        If check(x).Checked = True Then
                            chkBox04.Checked = True
                        End If
                    Next
                End If
            Case 14
            Case 15
                If chkBox15.Checked = True Then
                    For x = 15 To 19
                        check(x).Checked = True
                    Next
                Else
                    If chkBox16.Checked = True And chkBox17.Checked = True And
                        chkBox18.Checked = True And chkBox19.Checked = True Then

                        For x = 15 To 19
                            If check(x).Checked = True Then
                                check(x).Checked = False
                            End If
                        Next
                    End If
                End If
            Case 16 To 20
                If chkBox16.Checked = False Or chkBox17.Checked = False Or
                  chkBox18.Checked = False Or chkBox19.Checked = False Or chkBox20.Checked = False Then

                    chkBox15.Checked = False
                Else
                    For x = 15 To 19
                        If check(x).Checked = True Then
                            chkBox15.Checked = True
                        End If
                    Next
                End If
            Case 21
                If chkBox21.Checked = True Then
                    For x = 21 To 24
                        check(x).Checked = True
                    Next
                Else
                    If chkBox22.Checked = True And chkBox23.Checked = True And
                        chkBox24.Checked = True And chkBox25.Checked = True Then

                        For x = 21 To 24
                            If check(x).Checked = True Then
                                check(x).Checked = False
                            End If
                        Next
                    End If
                End If
            Case 22 To 25
                If chkBox22.Checked = False Or chkBox23.Checked = False Or
                 chkBox24.Checked = False Or chkBox25.Checked = False Then

                    chkBox21.Checked = False
                Else
                    For x = 21 To 24
                        If check(x).Checked = True Then
                            chkBox21.Checked = True
                        End If
                    Next
                End If
            Case 26
                If chkBox26.Checked = True Then
                    For x = 26 To 29
                        check(x).Checked = True
                    Next
                Else
                    If chkBox27.Checked = True And chkBox28.Checked = True And
                        chkBox29.Checked = True And chkBox30.Checked = True Then

                        For x = 26 To 29
                            If check(x).Checked = True Then
                                check(x).Checked = False
                            End If
                        Next
                    End If
                End If
            Case 27 To 30
                If chkBox27.Checked = False Or chkBox28.Checked = False Or
                 chkBox29.Checked = False Or chkBox30.Checked = False Then

                    chkBox26.Checked = False
                Else
                    For x = 26 To 29
                        If check(x).Checked = True Then
                            chkBox26.Checked = True
                        End If
                    Next
                End If
            Case 31
                If chkBox31.Checked = True Then
                    For x = 30 To 32
                        check(x).Checked = True
                    Next
                Else
                    If chkBox32.Checked = True And chkBox33.Checked = True Then

                        For x = 30 To 32
                            If check(x).Checked = True Then
                                check(x).Checked = False
                            End If
                        Next
                    End If
                End If
            Case 32 To 33
                If chkBox32.Checked = False Or chkBox33.Checked = False Then

                    chkBox31.Checked = False
                Else
                    For x = 30 To 32
                        If check(x).Checked = True Then
                            chkBox31.Checked = True
                        End If
                    Next
                End If
        End Select
        p_xResidence.present_address.sAddressx = IIf(chkBox02.Checked, "-1", "NULL")
        p_xResidence.primary_address.sAddressx = IIf(chkBox03.Checked, "-1", "NULL")

        p_xPropertyx.sProprty1 = IIf(chkBox05.Checked, "-1", "NULL")
        p_xPropertyx.sProprty2 = IIf(chkBox06.Checked, "-1", "NULL")
        p_xPropertyx.sProprty3 = IIf(chkBox07.Checked, "-1", "NULL")
        p_xPropertyx.cWith4Whl = IIf(chkBox08.Checked, "-1", "NULL")
        p_xPropertyx.cWith3Whl = IIf(chkBox09.Checked, "-1", "NULL")
        p_xPropertyx.cWith2Whl = IIf(chkBox10.Checked, "-1", "NULL")
        p_xPropertyx.cWithRefx = IIf(chkBox11.Checked, "-1", "NULL")
        p_xPropertyx.cWithTVxx = IIf(chkBox12.Checked, "-1", "NULL")
        p_xPropertyx.cWithACxx = IIf(chkBox13.Checked, "-1", "NULL")

        p_xMeansInfo.employed.sEmployer = IIf(chkBox16.Checked, "-1", "NULL")
        p_xMeansInfo.employed.sWrkAddrx = IIf(chkBox17.Checked, "-1", "NULL")
        p_xMeansInfo.employed.sPosition = IIf(chkBox18.Checked, "-1", "NULL")
        p_xMeansInfo.employed.nLenServc = IIf(chkBox19.Checked, -1.0, -10.0)
        p_xMeansInfo.employed.nSalaryxx = IIf(chkBox20.Checked, -1.0, -10.0)

        p_xMeansInfo.self_employed.sBusiness = IIf(chkBox22.Checked, "-1", "NULL")
        p_xMeansInfo.self_employed.sBusAddrx = IIf(chkBox23.Checked, "-1", "NULL")
        p_xMeansInfo.self_employed.nBusIncom = IIf(chkBox24.Checked, -1.0, -10.0)
        p_xMeansInfo.self_employed.nMonExpns = IIf(chkBox25.Checked, -1.0, -10.0)

        p_xMeansInfo.financed.sFinancer = IIf(chkBox27.Checked, "-1", "NULL")
        p_xMeansInfo.financed.sReltnDsc = IIf(chkBox28.Checked, "-1", "NULL")
        p_xMeansInfo.financed.sCntryNme = IIf(chkBox29.Checked, "-1", "NULL")
        p_xMeansInfo.financed.nEstIncme = IIf(chkBox30.Checked, -1.0, -10.0)

        p_xMeansInfo.pensioner.sPensionx = IIf(chkBox32.Checked, "-1", "NULL")
        p_xMeansInfo.pensioner.nPensionx = IIf(chkBox33.Checked, -1.0, -10.0)

    End Sub

    Private Sub txtField_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.F3 Or e.KeyCode = Keys.Enter Then
            Dim loTxt As Control
            loTxt = CType(sender, System.Windows.Forms.TextBox)

            loTxt = Nothing
            If TypeOf poControl Is TextBox Then
                loTxt = CType(poControl, System.Windows.Forms.TextBox)
            ElseIf TypeOf poControl Is CheckBox Then
                loTxt = CType(poControl, System.Windows.Forms.CheckBox)
            ElseIf TypeOf poControl Is ComboBox Then
                loTxt = CType(poControl, System.Windows.Forms.ComboBox)
            End If

            Dim lnCtr As Integer
            Dim loIndex As Integer
            loIndex = Val(Mid(loTxt.Name, 9))

            If Mid(loTxt.Name, 1, 8) = "txtField" Then
                Select Case loIndex
                    Case 1, 2
                        With poTrans
                            pnLoadx = 2

                            poTrans.TransNo = loTxt.Text

                            lnCtr = poTrans.isRecordExist()

                            If lnCtr = 1 Then
                                If poTrans.LoadRecord() Then
                                    MsgBox("Transaction already transfered to ci")
                                    Call clearFields()
                                End If
                            ElseIf lnCtr = 0 Then
                                poTrans.TransNo = loTxt.Text
                                If poTrans.NewRecord() Then
                                    Call ctrlFields()
                                    'get field values
                                    p_oResidence = poTrans.CI_Residence
                                    p_oPropertyx = poTrans.CI_Property
                                    p_oMeansInfo = poTrans.CI_Means_Info
                                    'get result field values
                                    p_xResidence = poTrans.Result_Residence
                                    p_xPropertyx = poTrans.Result_Property
                                    p_xMeansInfo = poTrans.Result_Means_Info

                                    Call clearFields()
                                    Call loadTransaction()
                                End If
                            End If
                        End With
                    Case 6
                        Dim lsValue As String = txtField06.Text.Trim
                        lsValue = poTrans.getCreditInvestigator(lsValue, True, False)

                        txtField06.Text = lsValue
                End Select
            End If

            If TypeOf poControl Is TextBox Then
                SelectNextControl(loTxt, True, True, True, True)
            End If
        End If
    End Sub

    Private Sub loadTransaction()
        Call clearFields()
        Call loadMainInfo(Me.Panel1)
        Call loadResInfo(Me.GroupBox1)
        Call loadPropertiesInfo(Me.GroupBox2)
        Call loadMIEmployed(Me.GroupBox5)
        Call loadMISelfemp(Me.GroupBox6)
        Call loadMIFinance(Me.GroupBox7)
        Call loadMIPension(Me.GroupBox8)

    End Sub
    Private Sub loadMainInfo(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadMainInfo(loTxt)
            ElseIf (TypeOf loTxt Is TextBox) Then
                Dim loIndex As Integer
                loIndex = Val(Mid(loTxt.Name, 9))
                If LCase(Mid(loTxt.Name, 1, 8)) = "txtField" Then
                    With poTrans
                        Select Case loIndex
                            Case 1
                                loTxt.Text = poTrans.Master("sTransNox")
                            Case 2
                                loTxt.Text = IFNull(.Master("sBranchNm"))
                            Case 3
                                txtField03.ReadOnly = True
                                loTxt.Text = .Master("sClientNm")
                            Case 4
                                txtField04.ReadOnly = True

                                loTxt.Text = Format(poTrans.Master("dTransact"), xsDATE_MEDIUM)
                            Case 5
                                txtField05.ReadOnly = True
                                loTxt.Text = poTrans.CI_Residence.present_address.sAddressx
                            Case 6
                                txtField05.ReadOnly = True
                                'If poTrans.Others("xCredInvx").ToString() = "" Then
                                '    loTxt.Text = ""
                                'Else
                                '    loTxt.Text = poTrans.Others("xCredInvx")
                                'End If

                        End Select
                    End With
                End If
            End If
        Next
    End Sub
    Private Sub loadResInfo(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadMainInfo(loTxt)
            ElseIf (TypeOf loTxt Is TextBox) Then
                Dim loIndex As Integer
                loIndex = Val(Mid(loTxt.Name, 9))
                If LCase(Mid(loTxt.Name, 1, 8)) = "txtField" Then
                    With poTrans
                        Select Case loIndex
                            Case 9
                                txtField09.ReadOnly = True
                                loTxt.Text = poTrans.CI_Residence.present_address.sAddressx
                            Case 10
                                txtField10.ReadOnly = True
                                loTxt.Text = poTrans.CI_Residence.primary_address.sAddressx
                        End Select
                    End With
                End If
            End If

        Next
    End Sub
    Private Sub loadPropertiesInfo(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadMainInfo(loTxt)
            ElseIf (TypeOf loTxt Is TextBox) Then
                Dim loIndex As Integer
                loIndex = Val(Mid(loTxt.Name, 9))
                If LCase(Mid(loTxt.Name, 1, 8)) = "txtField" Then

                    With poTrans
                        Select Case loIndex

                            Case 11
                                'txtField11.ReadOnly = True
                                loTxt.Text = poTrans.CI_Property.sProprty1
                                If loTxt.Text = "" Then
                                    chkBox05.Enabled = False
                                    chkBox04.Enabled = False
                                End If
                            Case 12
                                'txtField12.ReadOnly = True
                                loTxt.Text = poTrans.CI_Property.sProprty2
                                If loTxt.Text = "" Then
                                    chkBox06.Enabled = False
                                    chkBox04.Enabled = False
                                End If
                            Case 13
                                'txtField13.ReadOnly = True
                                loTxt.Text = poTrans.CI_Property.sProprty3
                                If loTxt.Text = "" Then
                                    chkBox07.Enabled = False
                                    chkBox04.Enabled = False
                                End If
                            Case 14
                                'txtField14.ReadOnly = True
                                If poTrans.CI_Property.cWith4Whl = "1" Then
                                    loTxt.Text = "YES"
                                ElseIf poTrans.CI_Property.cWith4Whl = "0" Then
                                    loTxt.Text = "NO"
                                    chkBox08.Enabled = False
                                    chkBox04.Enabled = False
                                End If
                            Case 15
                                'txtField15.ReadOnly = True
                                If poTrans.CI_Property.cWith3Whl = "1" Then
                                    loTxt.Text = "YES"
                                Else
                                    loTxt.Text = "NO"
                                    chkBox09.Enabled = False
                                    chkBox04.Enabled = False
                                End If
                            Case 16
                                'txtField16.ReadOnly = True
                                If poTrans.CI_Property.cWith2Whl = "1" Then
                                    loTxt.Text = "YES"
                                Else
                                    loTxt.Text = "NO"
                                    chkBox10.Enabled = False
                                    chkBox04.Enabled = False
                                End If
                            Case 17
                                'txtField17.ReadOnly = True
                                If poTrans.CI_Property.cWithRefx = "1" Then
                                    loTxt.Text = "YES"
                                Else
                                    loTxt.Text = "NO"
                                    chkBox11.Enabled = False
                                    chkBox04.Enabled = False
                                End If
                            Case 18
                                'txtField18.ReadOnly = True
                                If poTrans.CI_Property.cWithTVxx = "1" Then
                                    loTxt.Text = "YES"
                                Else
                                    loTxt.Text = "NO"
                                    chkBox12.Enabled = False
                                    chkBox04.Enabled = False
                                End If
                            Case 19
                                'txtField19.ReadOnly = True
                                If poTrans.CI_Property.cWithACxx = "1" Then
                                    loTxt.Text = "YES"
                                Else
                                    loTxt.Text = "NO"
                                    chkBox13.Enabled = False
                                    chkBox04.Enabled = False
                                End If

                        End Select
                    End With

                End If
            End If
        Next
    End Sub
    Private Sub loadMIEmployed(ByVal locontrol As Control)
        Dim lotxt As Control
        For Each lotxt In locontrol.Controls
            If lotxt.HasChildren Then
                Call loadMainInfo(lotxt)
            ElseIf (TypeOf lotxt Is TextBox) Then
                Dim loindex As Integer
                loindex = Val(Mid(lotxt.Name, 9))
                If LCase(Mid(lotxt.Name, 1, 8)) = "txtfield" Then
                    If poTrans.CI_Means_Info.employed Is Nothing Then
                        Select Case loindex
                            Case 20
                                chkBox15.Enabled = False
                                chkBox16.Enabled = False
                            Case 21
                                chkBox15.Enabled = False
                                chkBox17.Enabled = False
                            Case 22
                                chkBox15.Enabled = False
                                chkBox18.Enabled = False
                            Case 23
                                chkBox15.Enabled = False
                                chkBox19.Enabled = False
                            Case 24
                                chkBox15.Enabled = False
                                chkBox20.Enabled = False
                        End Select
                    Else
                        With poTrans
                            Select Case loindex
                                Case 20
                                    'txtField20.ReadOnly = True
                                    lotxt.Text = poTrans.CI_Means_Info.employed.sEmployer
                                Case 21
                                    'txtField21.ReadOnly = True
                                    lotxt.Text = poTrans.CI_Means_Info.employed.sWrkAddrx
                                Case 22
                                    'txtField22.ReadOnly = True
                                    lotxt.Text = poTrans.getPosition(poTrans.CI_Means_Info.employed.sPosition)
                                Case 23
                                    'txtField23.ReadOnly = True
                                    lotxt.Text = poTrans.CI_Means_Info.employed.nLenServc
                                Case 24
                                    'txtField24.ReadOnly = True
                                    'lotxt.Text = poTrans.CI_Means_Info.employed.nSalaryxx
                                    If Not IsNumeric(poTrans.CI_Means_Info.employed.nSalaryxx) Then
                                        lotxt.Text = poTrans.CI_Means_Info.employed.nSalaryxx.ToString()
                                        lotxt.Text = FormatNumber(CDbl(0), 2)
                                    Else
                                        lotxt.Text = poTrans.CI_Means_Info.employed.nSalaryxx
                                        lotxt.Text = FormatNumber(CDbl(lotxt.Text), 2)
                                    End If
                                    poTrans.CI_Means_Info.employed.nSalaryxx = CDbl(lotxt.Text)
                            End Select
                        End With
                    End If
                End If
                
            End If
        Next
    End Sub
    Private Sub loadMISelfemp(ByVal locontrol As Control)
       
            Dim lotxt As Control
            For Each lotxt In locontrol.Controls
                If lotxt.HasChildren Then
                    Call loadMainInfo(lotxt)
                ElseIf (TypeOf lotxt Is TextBox) Then
                    Dim loindex As Integer
                    loindex = Val(Mid(lotxt.Name, 9))

                    If LCase(Mid(lotxt.Name, 1, 8)) = "txtfield" Then
                    If poTrans.CI_Means_Info.self_employed Is Nothing Then
                        Select Case loindex
                            Case 25
                                chkBox21.Enabled = False
                                chkBox22.Enabled = False
                            Case 26
                                chkBox21.Enabled = False
                                chkBox23.Enabled = False
                            Case 27
                                chkBox21.Enabled = False
                                chkBox24.Enabled = False
                            Case 28
                                chkBox21.Enabled = False
                                chkBox25.Enabled = False
                        End Select
                    Else
                        With poTrans
                            Select Case loindex
                                Case 25
                                    lotxt.Text = poTrans.CI_Means_Info.self_employed.sBusiness
                                Case 26
                                    lotxt.Text = poTrans.CI_Means_Info.self_employed.sBusAddrx
                                Case 27
                                    If Not IsNumeric(poTrans.CI_Means_Info.self_employed.nBusIncom) Then
                                        lotxt.Text = poTrans.CI_Means_Info.self_employed.nBusIncom
                                        lotxt.Text = FormatNumber(CDbl(0), 2)
                                    Else
                                        lotxt.Text = poTrans.CI_Means_Info.self_employed.nBusIncom
                                        lotxt.Text = FormatNumber(CDbl(lotxt.Text), 2)
                                    End If
                                    poTrans.CI_Means_Info.self_employed.nBusIncom = CDbl(lotxt.Text)

                                Case 28
                                    If Not IsNumeric(poTrans.CI_Means_Info.self_employed.nMonExpns) Then
                                        lotxt.Text = poTrans.CI_Means_Info.self_employed.nMonExpns
                                        lotxt.Text = FormatNumber(CDbl(0), 2)
                                    Else
                                        lotxt.Text = poTrans.CI_Means_Info.self_employed.nMonExpns
                                        lotxt.Text = FormatNumber(CDbl(lotxt.Text), 2)
                                    End If
                                    poTrans.CI_Means_Info.self_employed.nMonExpns = CDbl(lotxt.Text)

                            End Select
                        End With
                    End If
                    End If
                End If
            Next

    End Sub


    Private Sub loadMIFinance(ByVal locontrol As Control)
        Dim lotxt As Control
        For Each lotxt In locontrol.Controls
            If lotxt.HasChildren Then
                Call loadMainInfo(lotxt)
            ElseIf (TypeOf lotxt Is TextBox) Then
                Dim loindex As Integer
                loindex = Val(Mid(lotxt.Name, 9))
                If LCase(Mid(lotxt.Name, 1, 8)) = "txtfield" Then
                    If poTrans.CI_Means_Info.financed Is Nothing Then
                        Select Case loindex
                            Case 29
                                chkBox26.Enabled = False
                                chkBox27.Enabled = False
                            Case 30
                                chkBox26.Enabled = False
                                chkBox28.Enabled = False
                            Case 31
                                chkBox26.Enabled = False
                                chkBox29.Enabled = False

                            Case 32
                                chkBox26.Enabled = False
                                chkBox30.Enabled = False
                        End Select
                    Else
                        With poTrans
                            Select Case loindex
                                Case 29
                                    lotxt.Text = poTrans.CI_Means_Info.financed.sFinancer
                                Case 30
                                    lotxt.Text = poTrans.CI_Means_Info.financed.sReltnDsc
                                Case 31
                                    lotxt.Text = poTrans.CI_Means_Info.financed.sCntryNme

                                Case 32
                                    If Not IsNumeric(poTrans.CI_Means_Info.financed.nEstIncme) Then
                                        lotxt.Text = poTrans.CI_Means_Info.financed.nEstIncme
                                        lotxt.Text = FormatNumber(CDbl(0), 2)
                                    Else
                                        lotxt.Text = poTrans.CI_Means_Info.financed.nEstIncme
                                        lotxt.Text = FormatNumber(CDbl(lotxt.Text), 2)
                                    End If
                                    poTrans.CI_Means_Info.financed.nEstIncme = CDbl(lotxt.Text)

                            End Select
                        End With
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub loadMIPension(ByVal locontrol As Control)
        Dim lotxt As Control
        For Each lotxt In locontrol.Controls
            If lotxt.HasChildren Then
                Call loadMainInfo(lotxt)
            ElseIf (TypeOf lotxt Is TextBox) Then
                Dim loindex As Integer
                loindex = Val(Mid(lotxt.Name, 9))
                If LCase(Mid(lotxt.Name, 1, 8)) = "txtfield" Then
                    If poTrans.CI_Means_Info.pensioner Is Nothing Then
                        Select Case loindex
                            Case 33
                                chkBox31.Enabled = False
                                chkBox32.Enabled = False
                            Case 34
                                chkBox31.Enabled = False
                                chkBox33.Enabled = False
                        End Select
                    Else
                        With poTrans
                            Select Case loindex
                                Case 33
                                    lotxt.Text = poTrans.CI_Means_Info.pensioner.sPensionx
                                Case 34
                                    If Not IsNumeric(poTrans.CI_Means_Info.pensioner.nPensionx) Then
                                        lotxt.Text = poTrans.CI_Means_Info.pensioner.nPensionx
                                        lotxt.Text = FormatNumber(CDbl(0), 2)
                                    Else
                                        lotxt.Text = poTrans.CI_Means_Info.pensioner.nPensionx
                                        lotxt.Text = FormatNumber(CDbl(lotxt.Text), 2)
                                    End If
                                    poTrans.CI_Means_Info.pensioner.nPensionx = CDbl(lotxt.Text)
                            End Select
                        End With
                    End If
                End If
                If lotxt.Text = "" Then
                    Select Case loindex
                        Case 33
                            chkBox31.Enabled = False
                            chkBox32.Enabled = False
                        Case 34
                            chkBox31.Enabled = False
                            chkBox33.Enabled = False
                    End Select
                End If
            End If
        Next
    End Sub


    Private Sub clearFields()
        Dim check() As CheckBox = {chkBox01, chkBox02, chkBox03, chkBox04, chkBox05, chkBox06, chkBox07, chkBox08, chkBox09, chkBox10,
                                      chkBox11, chkBox12, chkBox13, chkBox14, chkBox15, chkBox16, chkBox17, chkBox18, chkBox19, chkBox20,
                                      chkBox21, chkBox22, chkBox23, chkBox24, chkBox25, chkBox26, chkBox27, chkBox28, chkBox29, chkBox30,
                                      chkBox31, chkBox32, chkBox33}
        Dim txtVal() As TextBox = {txtField01, txtField02, txtField03, txtField04, txtField05, txtField06, txtField07, txtField08, txtField09, txtField10,
                                       txtField11, txtField12, txtField13, txtField14, txtField15, txtField16, txtField17, txtField18, txtField19, txtField20,
                                       txtField21, txtField22, txtField23, txtField24, txtField25, txtField26, txtField27, txtField28, txtField29, txtField30,
                                       txtField31, txtField32, txtField33, txtField34}
        Dim clrChk As Integer
        Dim clrTxtVal As Integer
        For clrChk = 0 To 32
            check(clrChk).Checked = False
        Next
        For clrTxtVal = 0 To 32
            txtVal(clrTxtVal).Text = ""
           
        Next
    End Sub

    Private Sub ctrlFields()
        Dim check() As CheckBox = {chkBox01, chkBox02, chkBox03, chkBox04, chkBox05, chkBox06, chkBox07, chkBox08, chkBox09, chkBox10,
                                      chkBox11, chkBox12, chkBox13, chkBox14, chkBox15, chkBox16, chkBox17, chkBox18, chkBox19, chkBox20,
                                      chkBox21, chkBox22, chkBox23, chkBox24, chkBox25, chkBox26, chkBox27, chkBox28, chkBox29, chkBox30,
                                      chkBox31, chkBox32, chkBox33}
        Dim txtVal() As TextBox = {txtField09, txtField10,
                                   txtField11, txtField12, txtField13, txtField14, txtField15, txtField16, txtField17, txtField18, txtField19, txtField20,
                                       txtField21, txtField22, txtField23, txtField24, txtField25, txtField26, txtField27, txtField28, txtField29, txtField30,
                                       txtField31, txtField32, txtField33, txtField34}
        Dim clrChk As Integer
        Dim clrTxtVal As Integer
        If pnLoadx = 1 Then
            For clrChk = 0 To 32
                check(clrChk).Enabled = False
            Next
            For clrTxtVal = 0 To 25
                txtVal(clrTxtVal).Enabled = False
            Next
        Else : pnLoadx = 2
            For clrChk = 0 To 32
                check(clrChk).Enabled = True
            Next
            For clrTxtVal = 0 To 25
                txtVal(clrTxtVal).Enabled = True
            Next
        End If
        
    End Sub

   

End Class