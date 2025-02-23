#!/bin/bash
git clone https://github.com/mkst/zte-config-utility.git zte
# python -m ensurepip --default-pip
pip install -U pyinstaller
# pip install auto-py-to-exe
pip install ./zte
pyinstaller --onefile --noconsole --icon=router.ico ./zte/examples/decode.py
mv ./dist/decode.exe ./zte.exe
rm -f decode.spec
rm -rf build
rm -rf dist
rm -rf zte
