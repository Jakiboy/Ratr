#!/bin/bash
G='\033[0;32m'

# Toolkits
echo -e "${G}Installing toolkits..."
python -m ensurepip --default-pip
pip install -U pyinstaller
sleep 2
clear

# ZTE
echo -e "${G}Generating zte.exe from source..."
git clone https://github.com/Jakiboy/Ztedecode.git zte
pip install ./zte
pyinstaller --onefile --noconsole --icon=router.ico ./zte/decode.py
mv ./dist/decode.exe ./zte.exe
rm -f decode.spec
rm -rf build dist zte
sleep 2
clear

# Huawei
echo -e "${G}Generating huawei.exe from source..."
git clone https://github.com/Jakiboy/Hwdecode.git huawei
pip install ./huawei
pyinstaller --onefile --noconsole --icon=router.ico --hidden-import=Crypto ./huawei/Hwdecode.py
mv ./dist/Hwdecode.exe ./huawei.exe
rm -f decode.spec
rm -rf build dist huawei
sleep 2
clear
