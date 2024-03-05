import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BajaPresupuestoComponent } from './baja-presupuesto.component';

describe('BajaPresupuestoComponent', () => {
  let component: BajaPresupuestoComponent;
  let fixture: ComponentFixture<BajaPresupuestoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BajaPresupuestoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BajaPresupuestoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
