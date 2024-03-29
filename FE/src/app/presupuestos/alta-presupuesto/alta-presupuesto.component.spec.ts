import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AltaPresupuestoComponent } from './alta-presupuesto.component';

describe('AltaPresupuestoComponent', () => {
  let component: AltaPresupuestoComponent;
  let fixture: ComponentFixture<AltaPresupuestoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AltaPresupuestoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AltaPresupuestoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
