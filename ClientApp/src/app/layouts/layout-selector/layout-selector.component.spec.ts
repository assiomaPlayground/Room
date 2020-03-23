import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LayoutSelectorComponent } from './layout-selector.component';

describe('LayoutSelectorComponent', () => {
  let component: LayoutSelectorComponent;
  let fixture: ComponentFixture<LayoutSelectorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LayoutSelectorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LayoutSelectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
