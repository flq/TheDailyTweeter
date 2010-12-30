# README #
## Licensing ##

Copyright 2010 Frank-Leonardo Quednau ([realfiction.net](http://realfiction.net)) 
Licensed under the Apache License, Version 2.0 (the "License"); 
you may not use this solution except in compliance with the License. 
You may obtain a copy of the License at 

http://www.apache.org/licenses/LICENSE-2.0 

Unless required by applicable law or agreed to in writing, 
software distributed under the License is distributed on an "AS IS" 
BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
See the License for the specific language governing permissions and limitations under the License. 

## Dependencies ##

Dependencies can be found in the "**lib**" subfolder. Some dependencies enter the system via 
[openwrap](http://www.openwrap.org). With time it is planned to let all deps enter that way.

To have more fun with the Observables, it is recommended you use the Reactive framework. The 2 important dlls are **System.Reactive.dll** and **System.CoreEx.dll**.
The additional WPF Application dependencies are contained in the **WpfApp subfolder** of the lib:

* [Caliburn.Micro.dll](http://caliburnmicro.codeplex.com/)
    * System.Windows.Interactivity.dll
* [Twitterizer2.dll](http://www.twitterizer.net/)
    * Twitterizer.Data.dll
    * [Newtonsoft.Json.dll](http://james.newtonking.com/pages/json-net.aspx)
* [StructureMap](http://structuremap.sourceforge.net/)

## Other stuff ##

### Annoying warning from IE on Twitter login screen? ###

See [here](http://blog.httpwatch.com/2009/04/23/fixing-the-ie-8-warning-do-you-want-to-view-only-the-webpage-content-that-was-delivered-securely/):

* Going  to Tools->Internet Options->Security
* Select the "Security" tab
* Click the "Custom Level" button
* In the "Miscellaneous" section change �Display mixed content� to Enable