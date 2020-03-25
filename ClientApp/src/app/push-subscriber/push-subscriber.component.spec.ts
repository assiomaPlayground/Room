import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PushSubscriberComponent } from './push-subscriber.component';

describe('PushSubscriberComponent', () => {
  let component: PushSubscriberComponent;
  let fixture: ComponentFixture<PushSubscriberComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PushSubscriberComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PushSubscriberComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
