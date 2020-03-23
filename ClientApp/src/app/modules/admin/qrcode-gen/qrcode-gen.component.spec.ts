import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QRCodeGenComponent } from './qrcode-gen.component';

describe('QRCodeGenComponent', () => {
  let component: QRCodeGenComponent;
  let fixture: ComponentFixture<QRCodeGenComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QRCodeGenComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QRCodeGenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
