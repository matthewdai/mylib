import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GoogleComboChartComponent } from './google-combo-chart.component';

describe('GoogleComboChartComponent', () => {
  let component: GoogleComboChartComponent;
  let fixture: ComponentFixture<GoogleComboChartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GoogleComboChartComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GoogleComboChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
