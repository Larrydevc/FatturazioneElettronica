del "C:\Proyectos VB.Net\FatturazioneElettronica\*.zip" /s /q
xcopy /s "C:\Proyectos VB.Net\FatturazioneElettronica\Output" "C:\Proyectos VB.Net\FatturazioneElettronica\Output2\"
del "C:\Proyectos VB.Net\FatturazioneElettronica\Output2\*.config" /s /q
del "C:\Proyectos VB.Net\FatturazioneElettronica\Output2\*.application" /s /q
del "C:\Proyectos VB.Net\FatturazioneElettronica\Output2\*.manifest" /s /q
del "C:\Proyectos VB.Net\FatturazioneElettronica\Output2\*.pdb" /s /q
del "C:\Proyectos VB.Net\FatturazioneElettronica\Output2\*.ini" /s /q
del "C:\Proyectos VB.Net\FatturazioneElettronica\Output2\*.xml" /s /q
del "C:\Proyectos VB.Net\FatturazioneElettronica\Output2\*.lnk" /s /q
@RD /S /Q "C:\Proyectos VB.Net\FatturazioneElettronica\Output2\Logs"
@RD /S /Q "C:\Proyectos VB.Net\FatturazioneElettronica\Output2\app.publish"
"C:\Program Files\7-Zip\7z" a -tzip "C:\Proyectos VB.Net\FatturazioneElettronica\FatturazioneElettronica.zip" "C:\Proyectos VB.Net\FatturazioneElettronica\Output2\*.*" -mx5
"C:\Program Files\7-Zip\7z" x "C:\Proyectos VB.Net\FatturazioneElettronica\FatturazioneElettronica.zip" -o"C:\Proyectos VB.Net\FatturazioneElettronica\FatturazioneElettronica" -aoa
@RD /S /Q "C:\Proyectos VB.Net\FatturazioneElettronica\Output2"
echo File FatturazioneElettronica.zip / Cartella FatturazioneElettronica creati
start %windir%\explorer.exe "C:\Proyectos VB.Net\FatturazioneElettronica\FatturazioneElettronica" 
pause