Imports ggcAppDriver
Imports ggcGOCAS
Imports ggcGOCAS.GOCASCI
Imports Newtonsoft.Json

Public Class frmCITaggingViewing
    Public poTrans As GOCASCI


    Private p_oResidence As residence_info
    Private p_oPropertyx As properties_info
    Private p_oMeansInfo As means_info

    Private p_xResidence As residence_info
    Private p_xPropertyx As properties_info
    Private p_xMeansInfo As means_info
    Private p_sTransNox As String

    Dim lnMsg As String
    Dim poControl As Control
    Dim pnLoadx As Integer
    Dim pnStat As Integer
    Private pxeModuleName As String = "Evaluator CI Tagging"
    Public WriteOnly Property sTransNox As String
        Set(ByVal value As String)
            p_sTransNox = value
        End Set
    End Property

    Private Sub FrmCITagging_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        
        If pnLoadx = 1 Then

            poTrans = New GOCASCI(p_oAppDriver)

            poTrans.TransNo = p_sTransNox
            With poTrans
                pnLoadx = 2
                If poTrans.isRecordExist() Then

                    If poTrans.LoadRecord() Then
                        Call loadTransaction()
                    End If

                End If
            End With
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


            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf txtField_KeyDown)
            pnLoadx = 1
        End If
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

            Dim loIndex As Integer
            loIndex = Val(Mid(loTxt.Name, 9))

            If Mid(loTxt.Name, 1, 8) = "txtField" Then
                Select Case loIndex
                    Case 1, 2

                        With poTrans
                            poTrans.TransNo = loTxt.Text
                            If poTrans.isRecordExist() Then

                                    If poTrans.LoadRecord() Then
                                    'get field values
                                    p_oResidence = poTrans.CI_Residence
                                    p_oPropertyx = poTrans.CI_Property
                                    p_oMeansInfo = poTrans.CI_Means_Info
                                    'get result field values
                                    p_xResidence = poTrans.Result_Residence
                                    p_xPropertyx = poTrans.Result_Property
                                    p_xMeansInfo = poTrans.Result_Means_Info


                                    Call loadTransaction()
                                End If
                            End If
                        End With

                End Select
            End If
            If TypeOf poControl Is TextBox Then
                SelectNextControl(loTxt, True, True, True, True)
            End If
        End If
    End Sub

    Private Sub loadTransaction()
        Call loadstatus()
        Call loadMainInfo(Me.Panel1)
        Call loadResInfo(Me.GroupBox1)
        Call loadRecomendation(Me.Panel11)
        Call loadPropertiesInfo(Me.GroupBox2)
        Call loadMIEmployed(Me.GroupBox5)
        Call loadMISelfemp(Me.GroupBox6)
        Call loadMIFinance(Me.GroupBox7)
        Call loadMIPension(Me.GroupBox8)
        Call loadCheckbox()
        Call loadAddInfo(Me.GroupBox3)
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
                                loTxt.Text = (poTrans.Others("sBranchNm"))
                            Case 3
                                txtField03.ReadOnly = True
                                loTxt.Text = .Master("sClientNm")
                            Case 4
                                txtField04.ReadOnly = True
                                'loTxt.Text = .Master("dTransact")
                                loTxt.Text = Format(poTrans.Master("dTransact"), xsDATE_MEDIUM)
                            Case 5
                                txtField05.ReadOnly = True
                                loTxt.Text = poTrans.CI_Residence.present_address.sAddressx
                            Case 6
                                If poTrans.Others("xCredInvx").ToString() = "" Then
                                    loTxt.Text = ""
                                Else
                                    loTxt.Text = (poTrans.Others("xCredInvx"))
                                End If

                        End Select
                    End With
                End If
            End If
        Next
    End Sub
    Private Sub loadRecomendation(ByVal loControl As Control)
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
                            Case 7
                                txtField09.ReadOnly = True
                                Select Case IFNull(poTrans.Others("cRcmdtnx1"), "-1")
                                    Case "1"
                                        loTxt.Text = "APPROVED"
                                    Case "0"
                                        loTxt.Text = "DISAPPROVED"
                                    Case Else
                                        loTxt.Text = "WAITING RESULT"
                                End Select
                            Case 8
                                txtField10.ReadOnly = True
                                Select Case IFNull(poTrans.Others("cRcmdtnx2"), "-1")
                                    Case "1"
                                        loTxt.Text = "APPROVED"
                                    Case "0"
                                        loTxt.Text = "DISAPPROVED"
                                    Case Else
                                        loTxt.Text = "WAITING RESULT"
                                End Select
                            Case 43
                                txtField10.ReadOnly = True
                                If poTrans.Others("sRcmdtnx1").ToString() = Nothing Then
                                    loTxt.Text = ""
                                Else
                                    loTxt.Text = poTrans.Others("sRcmdtnx1")
                                End If
                            Case 44
                                txtField10.ReadOnly = True
                                If poTrans.Others("sRcmdtnx2").ToString() = Nothing Then
                                    loTxt.Text = ""
                                Else
                                    loTxt.Text = poTrans.Others("sRcmdtnx2")
                                End If

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
                                txtField11.ReadOnly = True
                                loTxt.Text = poTrans.CI_Property.sProprty1
                            Case 12
                                txtField12.ReadOnly = True
                                loTxt.Text = poTrans.CI_Property.sProprty2
                            Case 13
                                txtField13.ReadOnly = True
                                loTxt.Text = poTrans.CI_Property.sProprty3
                            Case 14
                                txtField14.ReadOnly = True
                                If poTrans.CI_Property.cWith4Whl.ToString() = "1" Then
                                    loTxt.Text = "YES"
                                Else
                                    loTxt.Text = "NO"
                                End If
                            Case 15
                                txtField15.ReadOnly = True
                                If poTrans.CI_Property.cWith3Whl.ToString() = "1" Then
                                    loTxt.Text = "YES"
                                Else
                                    loTxt.Text = "NO"
                                End If
                            Case 16
                                txtField16.ReadOnly = True
                                If poTrans.CI_Property.cWith2Whl.ToString() = "1" Then
                                    loTxt.Text = "YES"
                                Else
                                    loTxt.Text = "NO"
                                End If
                            Case 17
                                txtField17.ReadOnly = True
                                If poTrans.CI_Property.cWithRefx.ToString() = "1" Then
                                    loTxt.Text = "YES"
                                Else
                                    loTxt.Text = "NO"
                                End If
                            Case 18
                                txtField18.ReadOnly = True
                                If poTrans.CI_Property.cWithTVxx.ToString() = "1" Then
                                    loTxt.Text = "YES"
                                Else
                                    loTxt.Text = "NO"
                                End If
                            Case 19
                                txtField19.ReadOnly = True
                                If poTrans.CI_Property.cWithACxx.ToString() = "1" Then
                                    loTxt.Text = "YES"
                                Else
                                    loTxt.Text = "NO"
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
                    With poTrans
                        Select Case loindex
                            Case 20
                                txtField20.ReadOnly = True
                                lotxt.Text = poTrans.CI_Means_Info.employed.sEmployer
                            Case 21
                                txtField21.ReadOnly = True
                                lotxt.Text = poTrans.CI_Means_Info.employed.sWrkAddrx
                            Case 22
                                txtField22.ReadOnly = True
                                lotxt.Text = poTrans.getPosition(poTrans.CI_Means_Info.employed.sPosition)
                            Case 23
                                txtField23.ReadOnly = True
                                 lotxt.Text = poTrans.CI_Means_Info.employed.nLenServc
                            Case 24
                                txtField24.ReadOnly = True
                                'lotxt.Text = poTrans.CI_Means_Info.employed.nSalaryxx
                                If Not IsNumeric(poTrans.CI_Means_Info.employed.nSalaryxx) Then
                                    lotxt.Text = poTrans.CI_Means_Info.employed.nSalaryxx
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
                    If String.IsNullOrEmpty(lotxt.Text) Then
                        lotxt.Text = ""
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
                    If String.IsNullOrEmpty(lotxt.Text) Then
                        lotxt.Text = ""
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
                    If String.IsNullOrEmpty(lotxt.Text) Then
                        lotxt.Text = ""
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
            End If
        Next
    End Sub

    Private Sub loadAddInfo(ByVal locontrol As Control)
        Dim lotxt As Control
        For Each lotxt In locontrol.Controls
            If lotxt.HasChildren Then
                Call loadMainInfo(lotxt)
            ElseIf (TypeOf lotxt Is TextBox) Then
                Dim loindex As Integer
                loindex = Val(Mid(lotxt.Name, 9))
                If LCase(Mid(lotxt.Name, 1, 8)) = "txtfield" Then
                    
                        With poTrans
                            Select Case loindex
                                Case 35
                                If poTrans.Others("cHasRecrd").ToString() = "1" Then
                                    lotxt.Text = "YES"
                                ElseIf poTrans.Others("cHasRecrd").ToString() = "" Then
                                    lotxt.Text = ""
                                Else
                                    lotxt.Text = "NO"
                                End If
                            Case 36
                                If poTrans.Others("sRecrdRem").ToString() = "" Then
                                    lotxt.Text = ""
                                Else
                                    lotxt.Text = poTrans.Others("sRecrdRem")
                                End If
                            Case 37
                                If poTrans.Others("sPrsnBrgy").ToString() = "" Then
                                    lotxt.Text = ""
                                Else
                                    lotxt.Text = poTrans.Others("sPrsnBrgy")
                                End If
                            Case 38
                                If poTrans.Others("sPrsnPstn").ToString() = "" Then
                                    lotxt.Text = ""
                                Else
                                    lotxt.Text = poTrans.Others("sPrsnPstn")
                                End If
                            Case 39
                                If poTrans.Others("sPrsnNmbr").ToString() = "" Then
                                    lotxt.Text = ""
                                Else
                                    lotxt.Text = poTrans.Others("sPrsnNmbr")
                                End If
                            Case 40
                                If poTrans.Others("sNeighbr1").ToString() = "" Then
                                    lotxt.Text = ""
                                Else
                                    lotxt.Text = poTrans.Others("sNeighbr1")
                                End If
                            Case 41
                                If poTrans.Others("sNeighbr2").ToString() = "" Then
                                    lotxt.Text = ""
                                Else
                                    lotxt.Text = poTrans.Others("sNeighbr2")
                                End If
                            Case 42
                                If poTrans.Others("sNeighbr3").ToString() = "" Then
                                    lotxt.Text = ""
                                Else
                                    lotxt.Text = poTrans.Others("sNeighbr3")
                                End If

                        End Select
                        End With
                End If
            End If
        Next
    End Sub
    Public Sub loadstatus()
        If poTrans.Others("cTranStat") = "0" Then
            Label40.Text = "OPEN"
            statBox1.Image = My.Resources.stat_pen
        ElseIf poTrans.Others("cTranStat") = "1" Then
            Label40.Text = "CLOSED"
            statBox1.Image = My.Resources.stat_pen
        ElseIf poTrans.Others("cTranStat") = "2" Then
            Label40.Text = "VERIFIED"
            statBox1.Image = My.Resources.stat_ver
        ElseIf poTrans.Others("cTranStat") = "3" Then
            Label40.Text = "CANCELLED"
            statBox1.Image = My.Resources.stat_rej
        Else
            Label40.Text = "UNKNOWN"
            statBox1.Image = My.Resources.stat_unk
        End If
    End Sub

    Public Sub loadCheckbox()

        p_xResidence = poTrans.Result_Residence
        p_xPropertyx = poTrans.Result_Property
        p_xMeansInfo = poTrans.Result_Means_Info

        Dim x As Integer
        Dim check() As CheckBox = {chkBox02, chkBox03, chkBox05, chkBox06, chkBox07,
                                   chkBox08, chkBox09, chkBox10, chkBox11, chkBox12,
                                   chkBox13, chkBox16, chkBox17, chkBox18, chkBox19,
                                   chkBox20, chkBox22, chkBox23, chkBox24, chkBox25,
                                   chkBox27, chkBox28, chkBox29, chkBox30, chkBox32, chkBox33}

        Dim pctbox() As PictureBox = {picBox1, picBox2, picBox3, picBox4, picBox5,
                                        picBox6, picBox7, picBox8, picBox9, picBox10,
                                        picBox11, picBox12, picBox13, picBox14, picBox15,
                                        picBox16, picBox17, picBox18, picBox19, picBox20,
                                      picBox21, picBox22, picBox23, picBox24, picBox25, picBox26}

        Dim statval() As String = {p_xResidence.present_address.sAddressx,
                                   p_xResidence.primary_address.sAddressx,
                                   p_xPropertyx.sProprty1,
                                   p_xPropertyx.sProprty2,
                                   p_xPropertyx.sProprty3,
                                   p_xPropertyx.cWith4Whl,
                                   p_xPropertyx.cWith3Whl,
                                   p_xPropertyx.cWith2Whl,
                                   p_xPropertyx.cWithRefx,
                                   p_xPropertyx.cWithTVxx,
                                   p_xPropertyx.cWithACxx,
                                   p_xMeansInfo.employed.sEmployer,
                                   p_xMeansInfo.employed.sWrkAddrx,
                                   p_xMeansInfo.employed.sPosition,
                                   p_xMeansInfo.employed.nLenServc,
                                   p_xMeansInfo.employed.nSalaryxx,
                                   p_xMeansInfo.self_employed.sBusiness,
                                   p_xMeansInfo.self_employed.sBusAddrx,
                                   p_xMeansInfo.self_employed.nBusIncom,
                                   p_xMeansInfo.self_employed.nMonExpns,
                                   p_xMeansInfo.financed.sFinancer,
                                   p_xMeansInfo.financed.sReltnDsc,
                                   p_xMeansInfo.financed.sCntryNme,
                                   p_xMeansInfo.financed.nEstIncme,
                                   p_xMeansInfo.pensioner.sPensionx,
                                   p_xMeansInfo.pensioner.nPensionx}

        For x = 0 To 25
           
            Dim value As String
            value = statval(x).ToString()
            Console.WriteLine(statval(x), value)
            If value = "0.0" Then
                value = "NULL"
            ElseIf value = "-1.0" Then
                value = "-1"
            ElseIf value = "1.0" Then
                value = "1"
            ElseIf value = "-10.0" Then
                value = "NULL"
            End If

            If value = "-1" Or value = "0" Or value = "1" Or value = "10" Or value = "20" Or value = "-10" Then
                check(x).Checked = True

                If value = "-1" Then
                    pctbox(x).Image = My.Resources.unknown
                ElseIf value = "0" Or value = "20" Then
                    pctbox(x).Image = My.Resources.wrong
                ElseIf value = "1" Or value = "10" Then
                    pctbox(x).Image = My.Resources.correct
                Else
                    pctbox(x).Image = Nothing
                End If
            Else
                check(x).Checked = False
                pctbox(x).Image = Nothing

            End If
        Next
    End Sub
End Class