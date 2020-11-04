@echo on

git status
pause

git add *
git add -u
git status
pause

set /p string=
git commit -m "%string%"

git push origin master
pause