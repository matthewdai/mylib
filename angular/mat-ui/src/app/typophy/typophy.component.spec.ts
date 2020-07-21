import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TypophyComponent } from './typophy.component';

describe('TypophyComponent', () => {
  let component: TypophyComponent;
  let fixture: ComponentFixture<TypophyComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TypophyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TypophyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
