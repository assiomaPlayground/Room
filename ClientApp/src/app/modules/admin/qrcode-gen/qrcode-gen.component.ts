import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-qrcode-gen',
  templateUrl: './qrcode-gen.component.html',
  styleUrls: ['./qrcode-gen.component.css']
})
export class QRCodeGenComponent {
  
  name = 'QRCodeGen';

  elementType : 'url' | 'canvas' | 'img' = 'url';
  value : string = 'facebook.com';
  }

