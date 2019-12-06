import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectStyleComponent } from './select-style.component';

describe('SelectStyleComponent', () => {
  let component: SelectStyleComponent;
  let fixture: ComponentFixture<SelectStyleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SelectStyleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SelectStyleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
