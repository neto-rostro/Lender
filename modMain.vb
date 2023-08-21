Imports ggcAppDriver
Imports System.Reflection
Imports System.Threading
Imports System.Text.RegularExpressions
Imports System.Drawing.Printing
Imports Newtonsoft.Json.Linq



Module modMain
    Public p_oAppDriver As GRider
    Public p_bShowd As Boolean
    Public p_isEvaluator As String
    Public p_sEvaluator As String = ""


    Private p_oThread1 As Thread
    Private WithEvents p_oMonitor As ggcGOCASMonitor.QMResult

    Private Declare Sub keybd_event Lib "user32.dll" (ByVal bVk As Byte, ByVal bScan As Byte, ByVal dwFlags As UInteger, ByVal dwExtraInfo As UInteger)

    Public Sub Main(ByVal args As String())
        'Enable XP visual style/skin
        Application.EnableVisualStyles()

        Dim lsProdctID As String
        Dim lsUserIDxx As String
        Dim lsIdentifier As String = ""

        If args.Length = 0 Then
            Dim loIni As New INIFile
            loIni.FileName = Environ("windir") & "\GRider.ini"
            If Not loIni.IsFileExist() Then
                MsgBox("Invalid Config File Detected!" & vbCrLf & "Verify your argument then try Again!", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical)
                Exit Sub
            End If

            lsProdctID = loIni.GetTextValue("Product", "ID")
            If lsProdctID = "LRTrackr" Then
                lsIdentifier = loIni.GetTextValue(lsProdctID, "ComputerID")
            End If
        Else
            lsProdctID = args(0)
            lsIdentifier = args(1)
        End If

        p_oAppDriver = New GRider(lsProdctID)

        If Not p_oAppDriver.LoadEnv() Then
            MsgBox("Unable to load configuration file!")
            Exit Sub
        End If

        If lsProdctID = "LRTrackr" Then
            Select Case lsIdentifier
                Case "S0"
                    p_sEvaluator = "True"
                Case "S1"
                    p_sEvaluator = "False"
            End Select

            'for janine
            'evaluator
            'p_sEvaluator = "True"
            'bisor()
            'p_sEvaluator = "False"
        End If

        'Auto Load Monitors before the user logs in
        If LCase(p_oAppDriver.ProductID) <> "integsys" And p_sEvaluator <> "" Then
            Call loadMonitor()
        End If

        'If Not p_oAppDriver.LogUser("M001130001") Then
        '    MsgBox("User unable to log!")
        '    Exit Sub
        'End If

        lsUserIDxx = ""
        If args.Length = 2 Then
            lsUserIDxx = args(1)
            If Not p_oAppDriver.LogUser(lsUserIDxx) Then
                MsgBox("User unable to log!")
                Exit Sub
            End If
        Else
            If Not p_oAppDriver.LogUser() Then
                MsgBox("User unable to log!")
                Exit Sub
            End If

        End If

        If lsProdctID = "LRTrackr" Then
            Select Case lsIdentifier
                Case "S0", "S1", "S2"
                    p_oAppDriver.MDI = mdiMain
                    mdiMain.ShowDialog()
                Case "S3"
                    p_oAppDriver.MDI = mdiCarTrade
                    mdiCarTrade.ShowDialog()
            End Select
        Else
            p_oAppDriver.MDI = mdiMain
            mdiMain.ShowDialog()
        End If
    End Sub

    'This method can handle all events using EventHandler
    Public Sub grpEventHandler(ByVal foParent As Control, ByVal foType As Type, ByVal fsGroupNme As String, ByVal fsEvent As String, ByVal foAddress As EventHandler)
        Dim loTxt As Control
        For Each loTxt In foParent.Controls
            If loTxt.GetType = foType Then
                'Handle events for this controls only
                If LCase(Mid(loTxt.Name, 1, Len(fsGroupNme))) = LCase(fsGroupNme) Then
                    If foType = GetType(TextBox) Then
                        Dim loObj = DirectCast(loTxt, TextBox)
                        Dim loEvent As EventInfo = foType.GetEvent(fsEvent)
                        loEvent.AddEventHandler(loObj, foAddress)
                    ElseIf foType = GetType(CheckBox) Then
                        Dim loObj = DirectCast(loTxt, CheckBox)
                        Dim loEvent As EventInfo = foType.GetEvent(fsEvent)
                        loEvent.AddEventHandler(loObj, foAddress)
                    ElseIf foType = GetType(Button) Then
                        Dim loObj = DirectCast(loTxt, Button)
                        Dim loEvent As EventInfo = foType.GetEvent(fsEvent)
                        loEvent.AddEventHandler(loObj, foAddress)
                    ElseIf foType = GetType(ComboBox) Then
                        Dim loObj = DirectCast(loTxt, ComboBox)
                        Dim loEvent As EventInfo = foType.GetEvent(fsEvent)
                        loEvent.AddEventHandler(loObj, foAddress)
                    ElseIf foType = GetType(RadioButton) Then
                        Dim loObj = DirectCast(loTxt, RadioButton)
                        Dim loEvent As EventInfo = foType.GetEvent(fsEvent)
                        loEvent.AddEventHandler(loObj, foAddress)
                    End If
                End If 'LCase(Mid(loTxt.Name, 1, 8)) = "txtfield"
            Else
                If loTxt.HasChildren Then
                    Call grpEventHandler(loTxt, foType, fsGroupNme, fsEvent, foAddress)
                End If
            End If
        Next 'loTxt In loControl.Controls
    End Sub


    'This method can handle all events using CancelEventHandler
    Public Sub grpCancelHandler(ByVal foParent As Control, ByVal foType As Type, ByVal fsGroupNme As String, ByVal fsEvent As String, ByVal foAddress As System.ComponentModel.CancelEventHandler)
        Dim loTxt As Control
        For Each loTxt In foParent.Controls
            If loTxt.GetType = foType Then
                'Handle events for this controls only
                If LCase(Mid(loTxt.Name, 1, Len(fsGroupNme))) = LCase(fsGroupNme) Then
                    If foType = GetType(TextBox) Then
                        Dim loObj = DirectCast(loTxt, TextBox)
                        Dim loEvent As EventInfo = foType.GetEvent(fsEvent)
                        loEvent.AddEventHandler(loObj, foAddress)
                    ElseIf foType = GetType(CheckBox) Then
                        Dim loObj = DirectCast(loTxt, CheckBox)
                        Dim loEvent As EventInfo = foType.GetEvent(fsEvent)
                        loEvent.AddEventHandler(loObj, foAddress)
                    ElseIf foType = GetType(Button) Then
                        Dim loObj = DirectCast(loTxt, Button)
                        Dim loEvent As EventInfo = foType.GetEvent(fsEvent)
                        loEvent.AddEventHandler(loObj, foAddress)
                    End If
                End If 'LCase(Mid(loTxt.Name, 1, 8)) = "txtfield"
            Else
                If loTxt.HasChildren Then
                    Call grpCancelHandler(loTxt, foType, fsGroupNme, fsEvent, foAddress)
                End If
            End If
        Next 'loTxt In loControl.Controls
    End Sub

    'This method can handle all events using KeyEventHandler
    Public Sub grpKeyHandler(ByVal foParent As Control, ByVal foType As Type, ByVal fsGroupNme As String, ByVal fsEvent As String, ByVal foAddress As KeyEventHandler)
        Dim loTxt As Control
        For Each loTxt In foParent.Controls
            If loTxt.GetType = foType Then
                'Handle events for this controls only
                If LCase(Mid(loTxt.Name, 1, Len(fsGroupNme))) = LCase(fsGroupNme) Then
                    If foType = GetType(TextBox) Then
                        Dim loObj = DirectCast(loTxt, TextBox)
                        Dim loEvent As EventInfo = foType.GetEvent(fsEvent)
                        loEvent.AddEventHandler(loObj, foAddress)
                    ElseIf foType = GetType(CheckBox) Then
                        Dim loObj = DirectCast(loTxt, CheckBox)
                        Dim loEvent As EventInfo = foType.GetEvent(fsEvent)
                        loEvent.AddEventHandler(loObj, foAddress)
                    ElseIf foType = GetType(Button) Then
                        Dim loObj = DirectCast(loTxt, Button)
                        Dim loEvent As EventInfo = foType.GetEvent(fsEvent)
                        loEvent.AddEventHandler(loObj, foAddress)
                    ElseIf foType = GetType(ComboBox) Then
                        Dim loObj = DirectCast(loTxt, ComboBox)
                        Dim loEvent As EventInfo = foType.GetEvent(fsEvent)
                        loEvent.AddEventHandler(loObj, foAddress)
                    End If
                End If 'LCase(Mid(loTxt.Name, 1, 8)) = "txtfield"
            Else
                If loTxt.HasChildren Then
                    Call grpKeyHandler(loTxt, foType, fsGroupNme, fsEvent, foAddress)
                End If
            End If
        Next 'loTxt In loControl.Controls
    End Sub

    'This method can handle all events using EventHandler
    Public Function FindTextBox(ByVal foParent As Control, ByVal fsName As String) As Control
        Dim loTxt As Control
        Static loRet As Control
        For Each loTxt In foParent.Controls
            If loTxt.GetType = GetType(TextBox) Then
                'Handle events for this controls only
                If LCase(loTxt.Name) = LCase(fsName) Then
                    loRet = loTxt
                End If
            Else
                If loTxt.HasChildren Then
                    Call FindTextBox(loTxt, fsName)
                End If
            End If
        Next 'loTxt In loControl.Controls

        Return loRet
    End Function

    Sub showMonitor()
        p_bShowd = True

        p_oThread1.Join()

        Call ShowCreditApp(p_oMonitor)
        frmMonitor.isEvaluator = p_isEvaluator
        frmMonitor.Left = (p_oAppDriver.MDI.Width - frmMonitor.Width) - 25
        frmMonitor.Top = (p_oAppDriver.MDI.Height - frmMonitor.Height) - 95

        If frmMonitor.dgvCreditApp.Rows(0).Cells(1).Value <> "" Then
            If frmMonitor.Visible = False Then
                frmMonitor.Show()
            End If
        Else
            If frmMonitor.Visible Then
                frmMonitor.Hide()
            End If
        End If
    End Sub

    Sub loadMonitor()
        p_bShowd = False

        p_oMonitor = New ggcGOCASMonitor.QMResult(p_oAppDriver)
        'for evaluation system its should be true and 1
        'for collector it shoud be false and 0

        p_oMonitor.isEvaluator = p_sEvaluator
        If p_sEvaluator = "True" Then
            p_isEvaluator = "1"
        ElseIf p_sEvaluator = "False" Then
            p_isEvaluator = "0"
        End If

        p_oThread1 = Nothing 'mac - free up memory
        p_oThread1 = New Thread(AddressOf p_oMonitor.ExtractRecord)
        p_oThread1.IsBackground = True
        p_oThread1.Start()
    End Sub

    Private Sub ShowCreditApp(ByVal loCreditApp As ggcGOCASMonitor.QMResult)
        With frmMonitor.dgvCreditApp
            .RowCount = 0
            frmMonitor.tbcMon.TabPages(0).Text = "Credit Online Application(" & loCreditApp.ItemCount & ")"
            If loCreditApp.ItemCount > 0 Then
                Dim lnCtr As Integer
                For lnCtr = 0 To loCreditApp.ItemCount - 1 Step 1
                    .Rows.Add()
                    .Rows(.Rows.Count - 1).Cells(0).Value = lnCtr + 1
                    .Rows(.Rows.Count - 1).Cells(1).Value = loCreditApp.Detail(lnCtr, "sAreaDesc")
                    .Rows(.Rows.Count - 1).Cells(2).Value = loCreditApp.Detail(lnCtr, "sBranchNm")
                    .Rows(.Rows.Count - 1).Cells(3).Value = loCreditApp.Detail(lnCtr, "sQMatchNo")
                    .Rows(.Rows.Count - 1).Cells(4).Value = IFNull(loCreditApp.Detail(lnCtr, "dReceived"), "")
                    .Rows(.Rows.Count - 1).Cells(5).Value = loCreditApp.Detail(lnCtr, "sClientNm")
                    .Rows(.Rows.Count - 1).Cells(6).Value = Format(loCreditApp.Detail(lnCtr, "dTransact"), xsDATE_MEDIUM)
                    .Rows(.Rows.Count - 1).Cells(7).Value = loCreditApp.Detail(lnCtr, "sTransNox")
                    '.Rows(.Rows.Count - 1).Cells(1).Value = loCreditApp.Detail(lnCtr, "sBranchNm")
                    '.Rows(.Rows.Count - 1).Cells(2).Value = loCreditApp.Detail(lnCtr, "sQMatchNo")
                    '.Rows(.Rows.Count - 1).Cells(3).Value = loCreditApp.Detail(lnCtr, "sClientNm")
                    '.Rows(.Rows.Count - 1).Cells(4).Value = Format(loCreditApp.Detail(lnCtr, "dTransact"), xsDATE_MEDIUM)
                    '.Rows(.Rows.Count - 1).Cells(5).Value = loCreditApp.Detail(lnCtr, "sTransNox")
                Next
            Else
                .Rows.Add()
            End If
        End With
    End Sub

    Public Sub SetNextFocus()
        keybd_event(&H9, 0, 0, 0)
        keybd_event(&H9, 0, &H2, 0)
    End Sub

    Public Sub SetPreviousFocus()
        keybd_event(&H10, 0, 0, 0)
        keybd_event(&H9, 0, 0, 0)
        keybd_event(&H10, 0, &H2, 0)
    End Sub

    Public Function isDatePosted(ByVal fdTranDate As Date) As Boolean
        Dim loDta As DataTable
        Dim lsSQL As String

        loDta = New DataTable
        loDta = ExecuteQuery("SELECT dUnEncode FROM Branch_Others WHERE sBranchCd = " & strParm(p_oAppDriver.BranchCode), p_oAppDriver.Connection)

        If loDta.Rows.Count = 0 Then
            Return False
            Exit Function
        Else
            If IsDBNull(loDta.Rows(0)("dUnEncode")) Then
                Return False
                Exit Function
            Else
                If CDate(fdTranDate) < CDate(loDta.Rows(0)("dUnEncode")) Then
                    Return False
                    Exit Function
                End If
            End If
        End If

        lsSQL = "SELECT" & _
                    " sTranDate" & _
                 " FROM DTR_Summary" & _
                 " WHERE sBranchCd = " & strParm(p_oAppDriver.BranchCode) & _
                    " AND sTranDate = " & strParm(Format(fdTranDate, "yyyyMdd")) & _
                 " ORDER BY sTranDate DESC" & _
                 " LIMIT 1"

        loDta = New DataTable
        loDta = ExecuteQuery(lsSQL, p_oAppDriver.Connection)

        If loDta.Rows.Count = 0 Then
            Return False
            Exit Function
        End If
        MsgBox(Format(fdTranDate, "yyyyMdd"))
        If loDta.Rows(0)("sTranDate") <= Format(fdTranDate, "yyyyMdd") Then
            MsgBox("Trasaction Date is not valid!!!" & vbCrLf & _
                     "Please verify your entry then try again!!!", vbCritical, "WARNING")
            isDatePosted = False
            Exit Function
        End If

        isDatePosted = True
    End Function

    Public Function isTransValid(ByVal fdTranDate As Date, _
                                     ByVal fsTranType As String, _
                                     ByVal fsReferNox As String, ByVal fnAmountx As Double) As Boolean
        Dim loDta As DataTable
        Dim lsSQL As String

        isTransValid = True

        loDta = New DataTable
        loDta = ExecuteQuery("SELECT dUnEncode FROM Branch_Others WHERE sBranchCd = " & strParm(p_oAppDriver.BranchCode), p_oAppDriver.Connection)

        If loDta.Rows.Count = 0 Then Exit Function
        If IsDBNull(loDta.Rows(0)("dUnEncode")) Then
            Exit Function
        Else
            If DateDiff("d", CDate(loDta.Rows(0)("dUnEncode")), fdTranDate) >= 0 Then
                'check the DTR_Summary here here

                lsSQL = "SELECT cPostedxx FROM DTR_Summary WHERE sBranchCd = " & strParm(p_oAppDriver.BranchCode) & _
                         " AND sTranDate = " & strParm(Format(fdTranDate, "yyyyMMdd"))
                loDta = New DataTable
                loDta = ExecuteQuery(lsSQL, p_oAppDriver.Connection)

                If loDta.Rows.Count = 0 Then
                    isTransValid = True
                Else
                    'if cPosted = 2, do not allow any transaction to encode
                    If loDta.Rows(0)("cPostedxx") = 2 Then
                        MsgBox("DTR Date was already posted!!!" & vbCrLf & _
                              "Please verify your entry then try again!!!", vbCritical, "WARNING")
                        isTransValid = False
                        'cposted = 1 then check referno to DTR_Summary_Detail
                    ElseIf loDta.Rows(0)("cPostedxx") = 1 Then
                        lsSQL = "SELECT b.cHasEntry, a.cPostedxx" & _
                           " FROM DTR_Summary a" & _
                           ", DTR_Summary_Detail b" & _
                           " WHERE a.sBranchCd = b.sBranchCd" & _
                           " AND a.sTranDate = b.sTranDate" & _
                           " AND a.sBranchCd = " & strParm(p_oAppDriver.BranchCode) & _
                           " AND a.sTranDate = " & strParm(Format(fdTranDate, "yyyyMMdd")) & _
                           " AND b.sTranType = " & strParm(fsTranType) & _
                           " AND b.sReferNox = " & strParm(fsReferNox) & _
                           " AND b.nTranAmtx = " & fnAmountx & _
                           " AND b.cHasEntry = " & strParm(0)

                        loDta = New DataTable
                        loDta = ExecuteQuery(lsSQL, p_oAppDriver.Connection)

                        If loDta.Rows.Count = 0 Then
                            MsgBox("No Reference no found from unencoded transaction!!" & vbCrLf & _
                                     " Pls check your entry then try again!!!")
                            isTransValid = False
                        ElseIf loDta.Rows(0)("cHasEntry") = 1 Then
                            MsgBox("Reference No was already posted!!!" & vbCrLf & _
                                    " Pls check your entry then try again!!!")
                            isTransValid = False
                        Else
                            isTransValid = True
                        End If
                    ElseIf loDta.Rows(0)("cPostedxx") = 0 Then
                        isTransValid = True
                    Else
                        isTransValid = False
                    End If
                End If
            Else
                isTransValid = False
                MsgBox("Unable to encode previous Transaction!!!" & vbCrLf & _
                         " Pls inform MIS/COMPLIANCE DEPT!!!", vbInformation, "WARNING")
            End If
        End If


        'loDta = New DataTable
        'loDta = ExecuteQuery("SELECT dUnEncode FROM Branch_Others WHERE sBranchCd = " & strParm(p_oAppDriver.BranchCode), p_oAppDriver.Connection)

        'If loDta.Rows.Count = 0 Then Exit Function
        'If IsDBNull(loDta.Rows(0)("dUnEncode")) Then
        '    Exit Function
        'Else
        '    If Format(loDta.Rows(0)("dUnEncode"), "yyyyMmd") < Format(fdTranDate, "yyyyMmd") Then Exit Function
        'End If


        'If Format(fdTranDate, "yyyyMmd") = Format(p_oAppDriver.SysDate, "yyyyMMdd") Then Exit Function

        'lsSQL = "SELECT" & _
        '            "  a.cPostedxx" & _
        '         " FROM DTR_Summary a" & _
        '            ", DTR_Summary_Detail b" & _
        '         " WHERE a.sBranchCd = b.sBranchCd" & _
        '            " AND a.sTranDate = b.sTranDate" & _
        '            " AND a.sBranchCd = " & strParm(p_oAppDriver.BranchCode) & _
        '            " AND a.sTranDate = " & strParm(Format(fdTranDate, "yyyyMmd")) & _
        '            " AND b.sTranType = " & strParm(fsTranType) & _
        '            " AND b.sReferNox = " & strParm(fsReferNox) & _
        '            " AND b.cHasEntry = " & strParm(xeLogical.NO)

        'loDta = New DataTable
        'loDta = ExecuteQuery(lsSQL, p_oAppDriver.Connection)
        'Debug.Print(lsSQL)

        'If loDta.Rows.Count <> 0 Then
        '    If loDta.Rows.Count > 1 Then
        '        MsgBox("Invalid Transaction detected!!!" & vbCrLf & _
        '                 "Multiple record found!!!", vbCritical, "WARNING")
        '        isTransValid = False
        '    Else
        '        If loDta.Rows(0)("cPostedxx") = xeTranStat.TRANS_POSTED Then
        '            MsgBox("Transaction date already posted!!!" & vbCrLf & _
        '                     "Please verify your entry then try again!!!", vbCritical, "WARNING")
        '            isTransValid = False
        '        End If
        '    End If
        'Else
        '    lsSQL = "SELECT" & _
        '             " cPostedxx" & _
        '          " FROM DTR_Summary" & _
        '          " WHERE sBranchCd = " & strParm(p_oAppDriver.BranchCode) & _
        '             " AND sTranDate = " & strParm(Format(fdTranDate, "yyyyMmd"))

        '    loDta = New DataTable
        '    loDta = ExecuteQuery(lsSQL, p_oAppDriver.Connection)

        '    If loDta.Rows.Count <> 0 Then
        '        If loDta.Rows(0)("cPostedxx") = xeTranStat.TRANS_POSTED Then
        '            MsgBox("Transaction date already posted!!!" & vbCrLf & _
        '                     "Please verify your entry then try again!!!", vbCritical, "WARNING")
        '            isTransValid = False
        '        Else
        '            MsgBox("Transaction is not yet encoded!!!" & vbCrLf & _
        '                        "Please verify your entry then try again!!!", vbCritical, "WARNING")
        '            isTransValid = False
        '        End If
        '    Else
        '        MsgBox("Transaction is not yet encoded!!!" & vbCrLf & _
        '                    "Please verify your entry then try again!!!", vbCritical, "WARNING")
        '        isTransValid = False
        '    End If
        'End If
    End Function
End Module




