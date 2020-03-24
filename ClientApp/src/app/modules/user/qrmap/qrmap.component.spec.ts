import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QrmapComponent } from './qrmap.component';

describe('QrmapComponent', () => {
  let component: QrmapComponent;
  let fixture: ComponentFixture<QrmapComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QrmapComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QrmapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
