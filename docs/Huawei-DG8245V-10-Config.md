# **Huawei DG8245V-10 Router/Gateway DSL Configuration Fix (Without Flash)**

## Overview

This tutorial walks you through the safe modification of your **Huawei DG8245V-10** router configuration (`config.xml`) to:

* **Disable TR-069 remote management**
* **Fully disable VOIP services**

These changes **enhance privacy and stability** by:

* Blocking ISP remote access
* Preventing unsolicited firmware/configuration updates
* Disabling potentially unstable or unused VOIP features

**Tested and working on Huawei DG8245V-10 – Region MA**

---

## Step-by-Step Configuration Guide

### Step 1: Export the Router Configuration File (`config.xml`)

1. Connect to your router interface:

   * Open your browser and navigate to `http://192.168.1.1`
2. Login with your **admin username and password**
3. Keep the admin dashboard open, then open a **new tab** and go to:

   * `http://192.168.1.1/html/ssmp/cfgfile/cfgfile.asp`
4. Enable hidden elements using your browser's Developer Console:

   ```javascript
   $('[style="display:none"]').removeAttr('style');
   ```
5. Click the download button to save the **`config.xml`** file to your computer.

---

### Step 2: Disable TR-069 Remote Management

TR-069 is used by ISPs to remotely control your router. Disabling it gives you **full privacy**.

**Modify the following in `config.xml`:**

* Change:

  ```xml
  <EnableCWMP>1</EnableCWMP>
  ```

  To:

  ```xml
  <EnableCWMP>0</EnableCWMP>
  ```

* Change:

  ```xml
  <PeriodicInformEnable>1</PeriodicInformEnable>
  ```

  To:

  ```xml
  <PeriodicInformEnable>0</PeriodicInformEnable>
  ```

* Set the ACS (Auto Configuration Server) URL to a dummy:

  ```xml
  <URL>http://0.0.0.0:7777/dsl</URL>
  ```

* Replace credentials with dummy placeholders:

  ```xml
  <Username>xxxxxx</Username>
  <Password>xxxxxx</Password>
  ```

* Set all `TR069FLAG` values under WAN connections to `0`

---

### Step 3: Disable VOIP Services

If you're not using VOIP, disabling it **improves DSL performance** and prevents VOIP reconfiguration by your ISP.

In the `config.xml`, make the following changes:

* Change all `VoiceProfileInstance` elements from:

  ```xml
  <Enable>Enabled</Enable>
  ```

  To:

  ```xml
  <Enable>Disabled</Enable>
  ```

* Disable both voice lines (usually InstanceID 1 and 2)

* Update SIP server addresses to local dummy values:

  ```xml
  <ProxyServer>127.0.0.1</ProxyServer>
  <RegistrarServer>127.0.0.1</RegistrarServer>
  <SIPPort>7777</SIPPort>
  ```

* Replace VOIP credentials:

  ```xml
  <AuthUserName>user@127.0.0.1</AuthUserName>
  <AuthPassword>xxxxxx</AuthPassword>
  ```

**Result**: No more VOIP interference, and router won't attempt VOIP registration.

---

### Step 4: Import the Edited Configuration File

Go to http://192.168.1.1/html/ssmp/cfgfile/cfgfile.asp in your browser, upload your edited config.xml file, and click "Update Configuration File" to save the changes.

---

## Technical Notes

* **DSL Internet remains fully functional**
* **VOIP features are completely disabled**
* **No remote ISP configuration possible**
* Keep your modified config.xml **private** – don't share it or upload it online!

---

## Reverting the Changes

If you ever need to go back:

1. Simply **reset the router** using the physical **RESET** button.
2. This will **restore factory settings**, including TR-069 and VOIP features.

---

## Summary

| Feature                  | Before      | After       |
| ------------------------ | ----------- | ----------- |
| TR-069 Remote Management | Enabled     | Disabled    |
| VOIP Functionality       | Active      | Disabled    |
| ISP Access               | Full        | Blocked     |
| DSL Internet             | ✅ Working | ✅ Working  |

With these modifications, your Huawei DG8245V-10 router is now **secure, stable**, and **under your full control**—**no flashing required**.
