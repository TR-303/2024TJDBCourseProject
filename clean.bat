@echo off
title 清理VS项目临时文件, 以便备份.
setlocal enabledelayedexpansion

rem 本BAT支持清理VS2017、VS2019、VS2022建立的项目，其它版本的VS项目请自行测试.
rem 可清理的项目类型: QT、MFC、Win32、控制台等项目的多余文件.
rem 本BAT不适合修改过目录设置的项目.

:: 可自行设置和参数
:: VS版本 vs2017=v15, vs2019=v16, vs2022=v17
set Version=v17

:: 勿动
set ProjectDir=%~f1
set ProjectName=%~nx1
if not exist !ProjectDir! (
    echo 不要双击本BAT文件!
    echo 请把要清理的项目文件夹拖到本BAT文件上!
) else (
    echo 清理的项目路径: %ProjectDir%
    
    rem 清理.vs目录
    del /f /q "!ProjectDir!\.vs\!ProjectName!\!Version!\*.db" 1>nul 2>nul
    rd /s /q "!ProjectDir!\.vs\!ProjectName!\!Version!\ipch\" 1>nul 2>nul
    rd /s /q "!ProjectDir!\.vs\!ProjectName!\FileContentIndex" 1>nul 2>nul
    rd /s /q "!ProjectDir!\.vs\!ProjectName!\.suo" 1>nul 2>nul
    
    rem 清理.aps文件, 这个文件特别大
    del /f /q "!ProjectDir!\!ProjectName!\*.aps" 1>nul 2>nul
    
    rem 清理生成的中间文件
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
    
    rem 清理生成的目录
    rd /s /q "!ProjectDir!\!ProjectName!\Release" 1>nul 2>nul
    rd /s /q "!ProjectDir!\!ProjectName!\Debug" 1>nul 2>nul
    rd /s /q "!ProjectDir!\!ProjectName!\x64" 1>nul 2>nul
    rd /s /q "!ProjectDir!\Release" 1>nul 2>nul
    rd /s /q "!ProjectDir!\Debug" 1>nul 2>nul
    rd /s /q "!ProjectDir!\x64" 1>nul 2>nul
    
    rem 清理解决方案文件夹中的临时文件
    rd /s /q "!ProjectDir!\.vs\" 1>nul 2>nul
    del /f /s /q "!ProjectDir!\*.VC.db" 1>nul 2>nul
    del /f /s /q "!ProjectDir!\*.VC.opendb" 1>nul 2>nul
)
pause & exit
