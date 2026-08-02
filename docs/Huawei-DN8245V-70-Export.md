This doc aims to show assist you in the extraction of the config file for the Huawei dn8245v-70. 


While trying to extract a config from a newer dn8245v-70, likely updated via your ISP, you may run into an issue that where commands given for huawei routers from the other docs do not work,and no download button appears,the following helper,will use leftover code that downloads the config file.

Steps

1. Login normally using your router IP(e.x 192.168.1.1)
2. Open a separate tab and go to (http://<ROUTER-IP>/html/ssmp/cfgfile/cfgfileroot.asp)
3. Press F12, go to the console and paste the following,

```
var tokenInput = document.getElementById('hwonttokenBackup');
if (!tokenInput) {
    console.error("Token element not found on this page!");
} else {
    var token = tokenInput.value;
    var form = document.createElement('form');
    form.method = 'POST';
    form.action = 'cfgfiledown.cgi?&RequestFile=html/ssmp/cfgfile/cfgfileroot.asp';

    var input = document.createElement('input');
    input.type = 'hidden';
    input.name = 'x.X_HW_Token';
    input.value = token;

    form.appendChild(input);
    document.body.appendChild(form);
    form.submit();
    document.body.removeChild(form);
}
```
The .xml config file should now start downloading.

