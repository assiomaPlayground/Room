import { Component, OnInit } from '@angular/core';
import { QrMap } from 'src/model/qrmap';
import { QrmapService } from 'src/service/qrmap.service';

@Component({
  selector: 'app-qrmap',
  templateUrl: './qrmap.component.html',
  styleUrls: ['./qrmap.component.css']
})
export class QrmapComponent implements OnInit {
    qrmap: QrMap[];
  constructor(private service: QrmapService) { }

  ngOnInit() {
    this.list()
    
  }
  list(){
    this.service.List().subscribe(qrmap=>this.qrmap=qrmap);
  }
  insert(qrmap: QrMap){
    this.service.insert(qrmap).subscribe(()=> this.list());
  }
  update(qrmap: QrMap){
    this.service.update(qrmap).subscribe(()=> this.list());
  }
  delete(qrmap: QrMap){
    this.service.delete(qrmap.Id).subscribe(()=> this.list());
  }
}
