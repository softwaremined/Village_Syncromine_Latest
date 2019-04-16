@echo off
set command=@Dns.SolutionBuilder.exe /v /p "." /control ".\Dns.SolutionBuilder.Control"
IF "%1" == "" (
	%command% ".\Mineware.Systems.Solution"
) else (
	%command% "%1"
)
