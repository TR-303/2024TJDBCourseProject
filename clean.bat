@echo off
title ����VS��Ŀ��ʱ�ļ�, �Ա㱸��.
setlocal enabledelayedexpansion

rem ��BAT֧������VS2017��VS2019��VS2022��������Ŀ�������汾��VS��Ŀ�����в���.
rem ���������Ŀ����: QT��MFC��Win32������̨����Ŀ�Ķ����ļ�.
rem ��BAT���ʺ��޸Ĺ�Ŀ¼���õ���Ŀ.

:: ���������úͲ���
:: VS�汾 vs2017=v15, vs2019=v16, vs2022=v17
set Version=v17

:: ��
set ProjectDir=%~f1
set ProjectName=%~nx1
if not exist !ProjectDir! (
    echo ��Ҫ˫����BAT�ļ�!
    echo ���Ҫ�������Ŀ�ļ����ϵ���BAT�ļ���!
) else (
    echo �������Ŀ·��: %ProjectDir%
    
    rem ����.vsĿ¼
    del /f /q "!ProjectDir!\.vs\!ProjectName!\!Version!\*.db" 1>nul 2>nul
    rd /s /q "!ProjectDir!\.vs\!ProjectName!\!Version!\ipch\" 1>nul 2>nul
    rd /s /q "!ProjectDir!\.vs\!ProjectName!\FileContentIndex" 1>nul 2>nul
    rd /s /q "!ProjectDir!\.vs\!ProjectName!\.suo" 1>nul 2>nul
    
    rem ����.aps�ļ�, ����ļ��ر��
    del /f /q "!ProjectDir!\!ProjectName!\*.aps" 1>nul 2>nul
    
    rem �������ɵ��м��ļ�
    del /f /s /q "!ProjectDir!\*.obj" 1>nul 2>nul
    del /f /s /q "!ProjectDir!\*.pch" 1>nul 2>nul
    del /f /s /q "!ProjectDir!\*.idb" 1>nul 2>nul
    del /f /s /q "!ProjectDir!\*.pdb" 1>nul 2>nul
    del /f /s /q "!ProjectDir!\*.tlog" 1>nul 2>nul
    del /f /s /q "!ProjectDir!\*.tmp" 1>nul 2>nul
    del /f /s /q "!ProjectDir!\*.log" 1>nul 2>nul
    del /f /s /q "!ProjectDir!\*.ipch" 1>nul 2>nul
    del /f /s /q "!ProjectDir!\*.iobj" 1>nul 2>nul
    del /f /s /q "!ProjectDir!\*.ipdb" 1>nul 2>nul
    del /f /s /q "!ProjectDir!\*.rsp" 1>nul 2>nul
    del /f /s /q "!ProjectDir!\*.ilk" 1>nul 2>nul
    del /f /s /q "!ProjectDir!\*.lastbuildstate" 1>nul 2>nul
    del /f /s /q "!ProjectDir!\*.dmp" 1>nul 2>nul
    
    rem �������ɵ�Ŀ¼
    rd /s /q "!ProjectDir!\!ProjectName!\Release" 1>nul 2>nul
    rd /s /q "!ProjectDir!\!ProjectName!\Debug" 1>nul 2>nul
    rd /s /q "!ProjectDir!\!ProjectName!\x64" 1>nul 2>nul
    rd /s /q "!ProjectDir!\Release" 1>nul 2>nul
    rd /s /q "!ProjectDir!\Debug" 1>nul 2>nul
    rd /s /q "!ProjectDir!\x64" 1>nul 2>nul
    
    rem �����������ļ����е���ʱ�ļ�
    rd /s /q "!ProjectDir!\.vs\" 1>nul 2>nul
    del /f /s /q "!ProjectDir!\*.VC.db" 1>nul 2>nul
    del /f /s /q "!ProjectDir!\*.VC.opendb" 1>nul 2>nul
)
pause & exit
