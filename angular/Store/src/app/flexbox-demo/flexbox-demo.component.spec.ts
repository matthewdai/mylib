import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FlexboxDemoComponent } from './flexbox-demo.component';

describe('FlexboxDemoComponent', () => {
  let component: FlexboxDemoComponent;
  let fixture: ComponentFixture<FlexboxDemoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FlexboxDemoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FlexboxDemoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
