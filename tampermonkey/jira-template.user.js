// ==UserScript==
// @name         Jira template script
// @namespace    thomas-lm.tampermonkey-script
// @version      1.0
// @description  create template for Jira issues
// @author       thomas-lm
// @match        https://URL_JIRA/*
// @grant        none

// @downloadURL https://github.com/thomas-lm/scripts/raw/master/tampermonkey/jira-template.user.js
// @updateURL   https://github.com/thomas-lm/scripts/raw/master/tampermonkey/jira-template.user.js
// ==/UserScript==
//
// modify URL_JIRA for mathing correct url corresponding to your jira url
//
(function() {
    'use strict';

    // templates
    var templates = {
      'desc' : '{panel:title=Description|borderStyle=solid|borderColor=#777|titleBGColor=#b3b3b3|titleColor=#333|bgColor=#efefef}\
* Description ...\
{panel}',
      'rules' : '{panel:title=Rules|borderStyle=solid|borderColor=#777|titleBGColor=#b3b3b3|titleColor=#333|bgColor=#efefef}\
# Rules ...\
{panel}'
    }

    // Init button
    function initButton() {
      var conteneursButton = document.querySelectorAll('.field-tools');
      if (conteneursButton) {
        conteneursButton.forEach(function(conteneur) {
          var textarea = conteneur.closest('form').querySelector('textarea.wiki-textfield')
          if(textarea && !conteneur.classList.contains('hasbuttontemplate')) {
            for (const [key, value] of Object.entries(templates)) {
              var elemClass = 'tampermk-tpl-id-' + key
              if(!conteneur.querySelector('.'+elemClass)) {
                console.log('add button',elemClass)
                var btn = document.createElement('button');
                btn.innerText = key;
                btn.class = elemClass;
                btn.addEventListener('click',function(e) {
                  textarea.value += '\r\n' + value;
                    textarea.scrollTop = textarea.scrollHeight
                  e.preventDefault();
                });
                conteneur.appendChild(btn);
              }
            }
            conteneur.classList.add('hasbuttontemplate')
          }
        });
      }
    }

    var watcherTimer = undefined;

    // Create a button for default template if 
    if(document.querySelector('body#jira')) {
      watcherTimer = window.setInterval(function() {
        initButton()
      },500);
    }
})();
