import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MatNavBarComponent } from './mat-nav-bar.component';

describe('MatNavBarComponent', () => {
  let component: MatNavBarComponent;
  let fixture: ComponentFixture<MatNavBarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MatNavBarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MatNavBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
